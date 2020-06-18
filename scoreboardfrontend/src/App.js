// src/App.js

import React from "react";
import ExternalApi from "./views/ExternalApi";

// New - import the React Router components, and the Profile page component

function App() {
  return (
    <div className="App">
      <ExternalApi />
    </div>
  );
}

export default App;