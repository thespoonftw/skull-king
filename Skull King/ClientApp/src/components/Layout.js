import React, { Component, useEffect, useState, useRef } from 'react';
import { Button } from 'reactstrap';
import * as signalR from "@microsoft/signalr";

export function Layout() {

  const userInputted = useRef();
  const [errorShown, setErrorShown] = useState();
  const [connection, setConnection] = useState(null);
  const [users, setUsers] = useState([]);
  const [user, setUser] = useState(null);
  useEffect( () => { createConnection(); }, [] )
  useEffect( () => { subscribeToUpdates(); }, [connection] )

  function createConnection() {
    const c = new signalR.HubConnectionBuilder()
    .withUrl("/userhub")
    .withAutomaticReconnect()
    .build();
    setConnection(c);
  }

  function subscribeToUpdates() {
    if (connection) {
      alert("making connections");
      connection.start().then(() => {
        connection.on("UsersUpdate", (msg) => { setUsers(msg); });
        connection.on("User", (msg) => { responseUser(msg); });
      });
    }
  }

  function updateInput(event) {
    userInputted.current = event.target.value;
    console.log(userInputted.current);
  }

  function responseUser(msg) {
    console.log(userInputted.current);
    if (msg) {
      setUser(userInputted.current);
    } else {
      setErrorShown(true);
    }
  }

  const createNewUser = async() => {
    console.log(userInputted.current);
    await connection.send("AddUser", userInputted.current)
  }

  return (
      <div>
        <h3>Skull King</h3>
        <br />
        {
          !user && <>
            <span>Input name:</span>
            <input type="text" onChange={updateInput} />
            <Button color="primary" onClick={createNewUser}>Join</Button>    
            { errorShown && 
              <p>Name already taken</p>
            }      
          </>
        }
        {
          user && <>
            <p>Logged in as:</p>
            <p>{user}</p>

            <p>Players ingame:</p>
            {
              users.map(u =>
                <p>{u}</p>
              )
            }
          </>
        }
      </div>
  );
}
