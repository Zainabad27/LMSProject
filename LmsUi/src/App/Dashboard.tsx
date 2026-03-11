import { useNavigate } from "react-router-dom";
import { useAuth } from "../Hooks/UseAuth";
import api from "../AxiosConfig";

const Dashboard = async () => {
  const nav = useNavigate();
  const user = await useAuth();
  if (user == null) {
    nav("/");
  }


 // if the user is teacher then our dashboard includes Assignments, Courses, Classes 



 

  return (
    <>
      <h1>Iam dashboard bro Dashboard</h1>
    </>
  );
};

export default Dashboard;
