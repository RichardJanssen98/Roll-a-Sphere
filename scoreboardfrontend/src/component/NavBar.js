// src/components/NavBar.js

import React, {useEffect} from "react";
import { useAuth0 } from "../react-auth0-spa";

const NavBar = () => {
  const { isAuthenticated, loginWithRedirect, logout, getIdTokenClaims } = useAuth0();

  const CheckIdToken = async () => {
    const idToken = await getIdTokenClaims();
    window.localStorage.setItem('accessToken', idToken.__raw);
  }

  useEffect(() => {
    window.localStorage.setItem('accessToken', 'lolNo');
    if(isAuthenticated)
    {
      CheckIdToken();
    }
  });

  

  return (
    <div>
      {!isAuthenticated && (
        <button onClick={() => loginWithRedirect({})}>Log in</button>
      )}

      {isAuthenticated && <button onClick={() => logout()}>Log out</button>}
    </div>
  );
};

export default NavBar;