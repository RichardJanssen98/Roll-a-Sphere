import { Table } from 'antd';
import "antd/dist/antd.css";
import React, { Component } from "react"

class MultipleSorterTable extends Component{
  constructor(props) {
    super(props);
    this.state = {playerScores: []}
  }
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
        compare: (a, b) => a.score - b.score,
        multiple: 3,
      },
    },
    {
      title: 'Time',
      dataIndex: 'time',
      key: 'time',
      sorter: {
        compare: (a, b) => a.time - b.time,
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