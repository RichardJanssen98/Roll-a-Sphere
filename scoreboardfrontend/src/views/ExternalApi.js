// src/views/ExternalApi.js

import React, { useState } from "react";
import { useAuth0 } from "../react-auth0-spa";
import MultipleSorterTable from "../components/MultipleSorterTable";

const ExternalApi = () => {
  const [showResult, setShowResult] = useState(false);
  const [apiMessage] = useState("");
  const { getIdTokenClaims } = useAuth0();
  const [responseData, setResponseData] = useState("");

  const callApi = async () => {
    try {
      const token = await getIdTokenClaims();

      const response = await fetch("http://localhost:27015/score/playerscores", {
        headers: {
          Authorization: `Bearer ${token.__raw}`
        }
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