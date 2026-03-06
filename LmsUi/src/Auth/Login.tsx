import { useForm } from "react-hook-form";
import { int, z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import loginSchema from "../ZodSchemas/LoginSchema";

// 2. Extract the type from the schema
type LoginFormInputs = z.infer<typeof loginSchema>;

interface LoginProps {
  LoginFor: string;
}

const LoginPage = (LoginProps: LoginProps) => {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormInputs>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormInputs) => {
    if (LoginProps.LoginFor === "Teacher") return;
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
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full rounded-lg bg-blue-600 py-3 font-semibold text-white transition-colors hover:bg-blue-700 disabled:bg-blue-400"
          >
            {isSubmitting ? "Logging in..." : "Login"}
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
