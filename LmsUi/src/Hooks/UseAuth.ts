import { useState } from "react";
import api from "../AxiosConfig"; 

interface User {
  role: string;
  name?: string;
}

export const useAuth = () => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  const fetchUser = async () => {
    try {
      setLoading(true);
      // The browser sends the cookie automatically because of withCredentials: true
      const response = await api.get<User>("/auth/me"); 
      setUser(response.data);
    } catch (error) {
      setUser(null);
    } finally {
      setLoading(false);
    }
  };

 

  return { user, loading, fetchUser };
};