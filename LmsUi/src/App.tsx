import { BrowserRouter, Routes, Route } from 'react-router-dom'
import LoginPage from './Auth/Login';
import SignupPage from './Auth/SignUp';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/" element={<LoginPage />} />
        <Route path="/signup" element={<SignupPage />} />
      </Routes>
    </BrowserRouter>
  )
}


export default App;