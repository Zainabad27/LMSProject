import { useState } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import  signupSchema from "../ZodSchemas/SignUpSchema";



type SignupFormData = z.infer<typeof signupSchema>;

type FilePreview = {
  name: string;
  url: string;
} | null;

export default function SignupPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirm, setShowConfirm] = useState(false);
  const [photoPrev, setPhotoPrev] = useState<FilePreview>(null);
  const [cnicFrontPrev, setCnicFrontPrev] = useState<FilePreview>(null);
  const [cnicBackPrev, setCnicBackPrev] = useState<FilePreview>(null);

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<SignupFormData>({
    resolver: zodResolver(signupSchema),
  });

  const handleFilePreview = (
    e: React.ChangeEvent<HTMLInputElement>,
    setter: (v: FilePreview) => void
  ) => {
    const file = e.target.files?.[0];
    if (file) {
      setter({ name: file.name, url: URL.createObjectURL(file) });
    }
  };

  const onSubmit = async (data: SignupFormData) => {
   

  };

  const inputClass = (hasError: boolean) =>
    `w-full px-4 py-3 rounded-lg border text-sm outline-none transition-all duration-200 bg-white
    ${hasError
      ? "border-red-400 focus:border-red-500 focus:ring-2 focus:ring-red-100"
      : "border-gray-200 focus:border-blue-500 focus:ring-2 focus:ring-blue-100"
    }`;

  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center px-4 py-10">
      <div className="bg-white rounded-2xl shadow-lg w-full max-w-2xl p-8">
        {/* Header */}
        <div className="text-center mb-8">
          <h1 className="text-3xl font-bold text-gray-900">Create Account</h1>
          <p className="text-gray-500 mt-1 text-sm">Please fill in your details to register</p>
        </div>

        <form onSubmit={handleSubmit(onSubmit)} noValidate>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-5">

            {/* School Name */}
            <div className="col-span-2 md:col-span-1">
              <label className="block text-sm font-medium text-gray-700 mb-1">School Name</label>
              <input
                {...register("schoolName")}
                placeholder="e.g. PSM School"
                className={inputClass(!!errors.schoolName)}
              />
              {errors.schoolName && (
                <p className="text-red-500 text-xs mt-1">{errors.schoolName.message}</p>
              )}
            </div>

            {/* Employee Name */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Employee Name</label>
              <input
                {...register("employeeName")}
                placeholder="e.g. Zain Ahmed"
                className={inputClass(!!errors.employeeName)}
              />
              {errors.employeeName && (
                <p className="text-red-500 text-xs mt-1">{errors.employeeName.message}</p>
              )}
            </div>

            {/* Email */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Email</label>
              <input
                {...register("email")}
                type="email"
                placeholder="you@example.com"
                className={inputClass(!!errors.email)}
              />
              {errors.email && (
                <p className="text-red-500 text-xs mt-1">{errors.email.message}</p>
              )}
            </div>

            {/* Contact */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Contact</label>
              <input
                {...register("contact")}
                placeholder="03363580957"
                className={inputClass(!!errors.contact)}
              />
              {errors.contact && (
                <p className="text-red-500 text-xs mt-1">{errors.contact.message}</p>
              )}
            </div>

            {/* Religion */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Religion <span className="text-gray-400 text-xs">(optional)</span>
              </label>
              <input
                {...register("religion")}
                placeholder="e.g. Islam"
                className={inputClass(!!errors.religion)}
              />
            </div>

            {/* Nationality */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Nationality <span className="text-gray-400 text-xs">(optional)</span>
              </label>
              <input
                {...register("nationality")}
                placeholder="e.g. Pakistani"
                className={inputClass(!!errors.nationality)}
              />
            </div>

            {/* Address */}
            <div className="col-span-2">
              <label className="block text-sm font-medium text-gray-700 mb-1">Address</label>
              <input
                {...register("address")}
                placeholder="e.g. Gulshan-e-Iqbal, Karachi"
                className={inputClass(!!errors.address)}
              />
              {errors.address && (
                <p className="text-red-500 text-xs mt-1">{errors.address.message}</p>
              )}
            </div>

            {/* Password */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Password</label>
              <div className="relative">
                <input
                  {...register("password")}
                  type={showPassword ? "text" : "password"}
                  placeholder="Min 8 chars, 1 uppercase, 1 number"
                  className={inputClass(!!errors.password) + " pr-10"}
                />
                <button
                  type="button"
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 text-xs"
                >
                  {showPassword ? "Hide" : "Show"}
                </button>
              </div>
              {errors.password && (
                <p className="text-red-500 text-xs mt-1">{errors.password.message}</p>
              )}
            </div>

            {/* Confirm Password */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Confirm Password</label>
              <div className="relative">
                <input
                  {...register("confirmPassword")}
                  type={showConfirm ? "text" : "password"}
                  placeholder="Re-enter password"
                  className={inputClass(!!errors.confirmPassword) + " pr-10"}
                />
                <button
                  type="button"
                  onClick={() => setShowConfirm(!showConfirm)}
                  className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 text-xs"
                >
                  {showConfirm ? "Hide" : "Show"}
                </button>
              </div>
              {errors.confirmPassword && (
                <p className="text-red-500 text-xs mt-1">{errors.confirmPassword.message}</p>
              )}
            </div>

            {/* File Uploads */}
            {[
              {
                label: "Profile Photo",
                field: "photo" as const,
                preview: photoPrev,
                setter: setPhotoPrev,
                error: errors.photo,
              },
              {
                label: "CNIC Front",
                field: "cnicFront" as const,
                preview: cnicFrontPrev,
                setter: setCnicFrontPrev,
                error: errors.cnicFront,
              },
              {
                label: "CNIC Back",
                field: "cnicBack" as const,
                preview: cnicBackPrev,
                setter: setCnicBackPrev,
                error: errors.cnicBack,
              },
            ].map(({ label, field, preview, setter, error }) => {
              const reg = register(field);
              return (
                <div key={field} className="col-span-2 md:col-span-1">
                  <label className="block text-sm font-medium text-gray-700 mb-1">{label}</label>
                  <label
                    className={`flex flex-col items-center justify-center w-full h-28 border-2 border-dashed rounded-lg cursor-pointer transition-colors
                      ${error ? "border-red-400 bg-red-50" : "border-gray-300 bg-gray-50 hover:bg-blue-50 hover:border-blue-400"}`}
                  >
                    {preview ? (
                      <div className="flex flex-col items-center gap-1">
                        <img
                          src={preview.url}
                          alt="preview"
                          className="h-14 w-20 object-cover rounded"
                        />
                        <span className="text-xs text-gray-500 truncate max-w-[140px]">{preview.name}</span>
                      </div>
                    ) : (
                      <div className="flex flex-col items-center gap-1 text-gray-400">
                        <svg className="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5}
                            d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
                        </svg>
                        <span className="text-xs">Click to upload</span>
                        <span className="text-xs text-gray-300">JPG, PNG, WEBP up to 5MB</span>
                      </div>
                    )}
                    <input
                      {...reg}
                      type="file"
                      accept="image/*"
                      className="hidden"
                      onChange={(e) => {
                        reg.onChange(e);
                        handleFilePreview(e, setter);
                      }}
                    />
                  </label>
                  {error && (
                    <p className="text-red-500 text-xs mt-1">{error.message as string}</p>
                  )}
                </div>
              );
            })}
          </div>

          {/* Submit */}
          <button
            type="submit"
            disabled={isSubmitting}
            className="mt-7 w-full bg-blue-600 hover:bg-blue-700 active:bg-blue-800 text-white font-semibold py-3 rounded-lg transition-colors duration-200 disabled:opacity-60 disabled:cursor-not-allowed text-base"
          >
            {isSubmitting ? "Creating Account..." : "Sign Up"}
          </button>

          <p className="text-center text-sm text-gray-500 mt-4">
            Already have an account?{" "}
            <a href="/login" className="text-blue-600 font-medium hover:underline">
              Login
            </a>
          </p>
        </form>
      </div>
    </div>
  );
}