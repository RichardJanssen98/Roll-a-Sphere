// src/App.js

import React from "react";
import NavBar from "./component/NavBar";
import { useAuth0 } from "./react-auth0-spa";
import Home from './index.tsx';

function App() {
  const { loading } = useAuth0();

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="App">
      <header>
        <NavBar />
      </header>
      <div> <Home/></div> 
    </div>
  );
}

export default App;