import { useState } from "react";
import api from "../AxiosConfig";

interface User {
  role: string;
  
}
const [user, setUser] = useState<User | null>(null);

export const useAuth = async () => {

  const fetchUser = async () => {
    try {
      // The browser sends the cookie automatically because of withCredentials: true
      const response = await api.get<User>("/auth/me");
      setUser(response.data);
    } catch (error) {
      setUser(null);
    } finally {
      setUser(null);
    }
  };


  await fetchUser();

  return { user };
};
