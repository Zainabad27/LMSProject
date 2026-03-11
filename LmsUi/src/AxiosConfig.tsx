import axios from 'axios';

const api = axios.create({
  // Use your actual .NET API URL here
  baseURL: 'http://localhost:5240/api/v1', 
  
  // This allows the browser to send/receive cookies automatically
  withCredentials: true, 
  
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;