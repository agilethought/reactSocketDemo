/**
 * Created 3/21/2017.
 * Copyright 2017
 */
import fetch from 'whatwg-fetch';
import io from 'socket.io-client';

const api = {

  listen: ()=>{
    const socket = io('http://localhost:15181/test?username=mike');
    socket.on('connect', function(){
      console.log('connected');
    });
    socket.on('event', function(data){
      console.log(`New data: ${JSON.stringify(data)}`);
    });
    socket.on('disconnect', function(){
      console.log('disconnect');
    });
  }

};


export default api;
