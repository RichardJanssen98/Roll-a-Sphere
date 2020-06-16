import React, { Component } from "react"
import { Button } from 'antd';
import MultipleSorterTable from "./component/MultipleSorterTable"
import { PlayerScore } from "./model/Content";
import "./App.css";
import ApiCaller from "./api/GateWay";

class Home extends Component{
    state = {
        playerScores: [],
    };

    render() {
        return (
            <div className="App">
                <h1>Roll a Sphere Scoreboard</h1>
                <MultipleSorterTable playerScores={this.state.playerScores} />
                <Button onClick={() => (this.loadshit(this))}>Get Scores</Button>
            </div> 
        );
    }

    loadshit(component: Component) {
        let scoreboardAPI = new ApiCaller("27015");

        scoreboardAPI.get('playerscores').then( (response) => {
            component.setState({ playerScores: response.data as PlayerScore[]})
        })
    }

}
export default Home;