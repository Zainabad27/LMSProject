import axios, { AxiosError } from "axios";
import React from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "../../Toast";

const Header: React.FC = () => {
  const nav= useNavigate();
  const handleLogout =async () => {
    // Logic for clearing tokens/state goes here
    console.log("Logged out");
    try {
    const result=await axios.post(  `http://localhost:5240/api/v1/Auth/Logout`);
    if(result.status==200){
      nav("/");

    }
      
      
    } catch (error: AxiosError|any) {
      console.error(error)
      toast.error(error.response?.data ||"Error Occured while logging out.");
      
    }



  };

  return (
    <header className="fixed top-0 left-0 w-full bg-white border-b border-gray-100 shadow-sm z-50">
      <div className="max-w-7xl mx-auto px-4 h-16 flex items-center justify-between">
        {/* Brand/Logo Area */}
        <div className="flex items-center space-x-2">
          <div className="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center">
            <span className="text-white font-bold text-sm">L</span>
          </div>
          <h1 className="text-xl font-bold text-slate-800 tracking-tight">
            LMS<span className="font-normal text-slate-500">ui</span>
          </h1>
        </div>

        {/* Action Area */}
        <div className="flex items-center gap-4">
          <button
            onClick={handleLogout}
            className="bg-[#2563eb] hover:bg-blue-700 text-white px-5 py-2 rounded-lg text-sm font-medium transition-all duration-200 shadow-sm active:scale-95"
          >
            Logout
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;
