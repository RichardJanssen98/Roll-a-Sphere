import { Table, Input, Button, Space } from 'antd';
import React, { Component } from "react"
import { PlayerScore } from "../model/Content";

class MultipleSorterTable extends Component<{
  playerScores: PlayerScore[],
}>{
  state = {
    playerScores: this.props.playerScores,
};

render() {
  const columns = [
    {
      title: 'Level',
      dataIndex: 'level',
      key: 'level'
    },
    {
      title: 'Score',
      dataIndex: 'score',
      key: 'score',
      sorter: {
        compare: (a: { score: number; }, b: { score: number; }) => a.score - b.score,
        multiple: 3,
      },
    },
    {
      title: 'Time',
      dataIndex: 'time',
      key: 'time',
      sorter: {
        compare: (a: { time: number; }, b: { time: number; }) => a.time - b.time,
        multiple: 2,
      },
    },
    {
      title: 'Email Player',
      dataIndex: 'emailPlayer',
      key: 'emailPlayer'
    },
    {
      title: "Username",
      dataIndex: 'username',
      key: 'username'
    },
  ];
  
  return <Table columns={columns} dataSource={this.props.playerScores} />;
  }
}

export default MultipleSorterTable