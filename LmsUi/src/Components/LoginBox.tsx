interface LoginBoxProps {
  LoginFor: string;
}

const LoginBox = (props: LoginBoxProps) => {
  const handleLoginClick =  () => {

    
   
    
  };
  return (
    <div
      onClick={() => {
        handleLoginClick();
      }}
      className="
        flex items-center justify-center
        w-48 h-48
        rounded-2xl
        cursor-pointer
        select-none
        transition-all duration-300 ease-in-out
        shadow-md hover:shadow-xl
        hover:-translate-y-1 hover:scale-105
      "
      style={{
        background:
          "linear-gradient(135deg, #e0f2fe 0%, #bae6fd 60%, #7dd3fc 100%)",
        border: "1.5px solid #93c5fd",
      }}
    >
      <div className="flex flex-col items-center gap-2">
        {/* Teacher Icon */}
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="w-10 h-10"
          viewBox="0 0 24 24"
          fill="none"
          stroke="#2563eb"
          strokeWidth="1.6"
          strokeLinecap="round"
          strokeLinejoin="round"
        >
          <path d="M12 12c2.7 0 4.8-2.1 4.8-4.8S14.7 2.4 12 2.4 7.2 4.5 7.2 7.2 9.3 12 12 12z" />
          <path d="M4 21c0-4.4 3.6-8 8-8s8 3.6 8 8" />
        </svg>

        {/* Label */}
        <span
          className="text-base font-semibold tracking-wide"
          style={{ color: "#1d4ed8" }}
        >
          {props.LoginFor}
        </span>

        {/* Login Button */}
        <span
          className="
            mt-1 px-5 py-1.5
            rounded-full
            text-xs font-bold tracking-widest uppercase
            transition-all duration-200
          "
          style={{
            background: "#2563eb",
            color: "#ffffff",
            letterSpacing: "0.15em",
          }}
        >
          Login
        </span>
      </div>
    </div>
  );
};

export default LoginBox;
