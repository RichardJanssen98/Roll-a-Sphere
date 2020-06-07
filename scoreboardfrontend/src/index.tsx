import React, { Component } from "react"
import { Row, Col, Button } from 'antd';
import MultipleSorterTable from "./component/MultipleSorterTable"
import { PlayerScore } from "./model/Content";
import ReactDOM from 'react-dom';
import "./App.css";
import ApiCaller from "./api/GateWay";

class Home extends Component{
    state = {
        playerScores: [],
    };

    playerScores: PlayerScore[] = [];

    render() {
        return (
            <div className="App">
                <h1>Roll a Sphere Scoreboard</h1>
                <MultipleSorterTable playerScores={this.state.playerScores} />
            </div> 
        );
    }

    async componentDidMount(): Promise<void> {
        let component = this;
        let scoreboardAPI = new ApiCaller("27015");

        scoreboardAPI.get('playerscores').then(function (response) {
            component.playerScores = response.data as PlayerScore[];
            component.setState({ playerScores: response.data as PlayerScore[]})
        });
    }
}
export default Home;