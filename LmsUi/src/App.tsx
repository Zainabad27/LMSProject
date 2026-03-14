import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./Auth/Login";
import SignupPage from "./Auth/SignUp";
import AppLayout from "./Components/AppLayout/Layout";
import Dashboard from "./App/AdminDashboard";
import Assignments from "./App/Assignments";
import AdminClassesPage from "./Pages/AdminClasses";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/" element={<LoginPage />} />
        <Route path="/signup" element={<SignupPage />} />

        <Route element={<AppLayout />}>
          <Route path="/App/Dashboard" element={<Dashboard />} />

          <Route path="/App/Assignments" element={<Assignments />} />

          <Route path="Admin/Classes" element={<AdminClassesPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
