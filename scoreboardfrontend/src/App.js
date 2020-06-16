// src/App.js

import React from "react";
import NavBar from "./components/NavBar";
import { useAuth0 } from "./react-auth0-spa";

// New - import the React Router components, and the Profile page component
import { Router, Route, Switch } from "react-router-dom";
import Profile from "./components/Profile";
import history from "./utils/history";
import ExternalApi from "./views/ExternalApi";
import PrivateRoute from "./components/PrivateRoute";

function App() {
  return (
    <div className="App">
      <Router history={history}>
        <header>
          <NavBar />
        </header>
        <Switch>
      <Route path="/" exact />
      <PrivateRoute path="/profile" component={Profile} />

      <PrivateRoute path="/scoreboard" component={ExternalApi} />
    </Switch>
      </Router>
    </div>
  );
}

export default App;