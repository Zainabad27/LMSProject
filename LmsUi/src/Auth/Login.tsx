import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import loginSchema from "../ZodSchemas/LoginSchema";

import { Link, useNavigate } from "react-router-dom";
import  { AxiosError } from "axios";
import { toast } from "../Toast";
import api from "../AxiosConfig";

// 2. Extract the type from the schema
type LoginFormInputs = z.infer<typeof loginSchema>;

const LoginPage = () => {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormInputs>({
    resolver: zodResolver(loginSchema),
  });

  const selectedRole = watch("role"); // watch to highlight selected

  const roles = ["Student", "Teacher", "Admin"];

  const onSubmit = async (data: LoginFormInputs) => {
    try {
      const response = await api.post(`/Auth/Login/${data.role}`, {
        email: data.email,
        password: data.password,
      });

      if (response.status === 200) {
        toast.success("Login successful!");
        // Redirect or perform other actions on successful login
        navigate("/app/dashboard");
      }
    } catch (error: AxiosError | any) {
      console.error("Login failed:", error);
      console.error(error.response?.data?.message || error.message);
      console.log("yeh hai detail bro.");
      console.log(error.response?.data);

      toast.error(
        error.response?.data ||
          error.message ||
          "Login failed. Please try again.",
      );
    }
  };

  return (
    <div className="flex h-screen w-screen items-center justify-center bg-gray-100 px-4">
      <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-2xl">
        <div className="mb-8 text-center">
          <h2 className="text-3xl font-bold text-gray-800">Welcome Back</h2>
          <p className="text-gray-500">Please enter your details</p>
        </div>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          {/* Email Field */}
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Email
            </label>
            <input
              {...register("email")}
              className={`mt-1 w-full rounded-lg border p-3 outline-none transition-all ${
                errors.email
                  ? "border-red-500 focus:ring-1 focus:ring-red-500"
                  : "border-gray-300 focus:border-blue-500"
              }`}
              placeholder="prof@university.edu"
            />
            {errors.email && (
              <p className="mt-1 text-xs text-red-500">
                {errors.email.message}
              </p>
            )}
          </div>

          {/* Password Field */}
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Password
            </label>
            <input
              type="password"
              {...register("password")}
              className={`mt-1 w-full rounded-lg border p-3 outline-none transition-all ${
                errors.password
                  ? "border-red-500 focus:ring-1 focus:ring-red-500"
                  : "border-gray-300 focus:border-blue-500"
              }`}
              placeholder="••••••••"
            />
            {errors.password && (
              <p className="mt-1 text-xs text-red-500">
                {errors.password.message}
              </p>
            )}
            <div style={{ display: "flex", gap: "20px" }}>
              {roles.map((role) => (
                <label
                  key={role}
                  style={{
                    display: "flex",
                    alignItems: "center",
                    gap: "8px",
                    cursor: "pointer",
                  }}
                >
                  <input
                    type="radio"
                    value={role.toLowerCase()}
                    {...register("role", { required: "Select a role" })}
                    style={{ display: "none" }}
                  />
                  {/* THE DOT */}
                  <div
                    style={{
                      width: "18px",
                      height: "18px",
                      borderRadius: "50%",
                      background:
                        selectedRole === role.toLowerCase()
                          ? "#3b82f6"
                          : "transparent",
                      border: "2px solid #3b82f6",
                      transition: "background 0.2s",
                    }}
                  />
                  {role}
                </label>
              ))}
            </div>
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full rounded-lg bg-blue-600 py-3 font-semibold text-white transition-colors hover:bg-blue-700 disabled:bg-blue-400"
            style={{
              cursor: "pointer",
            }}
          >
            {isSubmitting ? "Logging in..." : "Login"}
          </button>
        </form>
        <p className="text-center text-sm text-gray-500 mt-4">
          Don't have an account?{" "}
          <Link
            to="/signup"
            className="text-blue-600 font-medium hover:underline"
          >
            Create one here
          </Link>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;
