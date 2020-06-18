// src/views/ExternalApi.js

import React, { useState } from "react";
import MultipleSorterTable from "../components/MultipleSorterTable";

const ExternalApi = () => {
  const [showResult, setShowResult] = useState(false);
  const [apiMessage] = useState("");
  const [responseData, setResponseData] = useState("");

  const callApi = async () => {
    try {

      const response = await fetch(window.location.origin + "/api/score/playerscores", {
      });

      setResponseData(await response.json());

      setShowResult(true);
      //setApiMessage(responseData);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <>
      <h1>Scoreboard</h1>
      <button onClick={callApi}>Ping API</button>
      <MultipleSorterTable playerScores={responseData}></MultipleSorterTable>
      {showResult && <code>{JSON.stringify(apiMessage, null, 2)}</code>}
    </>
  );
};

export default ExternalApi;