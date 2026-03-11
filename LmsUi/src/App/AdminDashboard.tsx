import { useNavigate } from "react-router-dom";
import { useAuth } from "../Hooks/UseAuth";
import api from "../AxiosConfig";

const Dashboard = async () => {
  const nav = useNavigate();
  const data = await useAuth();
  if (data.user == null) {
    nav("/");
  } else if (data.user?.role != "Admin") {
    nav(`${data.user.role}Dashboard`);
  }


  

  return (
    <>
      <h1>Iam dashboard bro Dashboard</h1>
    </>
  );
};

export default Dashboard;
