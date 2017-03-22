import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Websocket from 'react-websocket';

class App extends Component {
  constructor(...args){
    super(...args);
    this.state = {
      name: ''
    };
  }

  handleData(data) {
    let result = JSON.parse(data);
    this.setState({name: result.name});
  }
  render() {

    return (
      <div className="App">
        <div className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h2>Welcome to React</h2>
        </div>
        <p className="App-intro">
          To get started, edit <code>src/App.js</code> and save to reload.
        </p>
        <div>
          <Websocket url="ws://localhost:15181/api/Test?username=mike"
                     onMessage={this.handleData.bind(this)} />
          Count: <strong>{this.state.name}</strong>
        </div>
      </div>
    );
  }
}

export default App;
