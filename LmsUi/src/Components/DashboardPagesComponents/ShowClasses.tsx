import ClassCard from "./ClassesCard";
import useClasses from "../../Hooks/UseClasses";
import { useState } from "react";
import api from "../../AxiosConfig";
import AddClassCard from "./AddClassCard";
import { useForm } from "react-hook-form";
import { X } from "lucide-react";
import type { ClassFormData } from "../../ZodSchemas/AddAClassForm";
import { toast } from "../../Toast";

interface ShowClassesProps {
  SchoolId: string;
}
interface EmptyStateProps {
  msg: string;
  description?: string;
}

const LoadingState = () => (
  <div className="flex justify-center items-center w-full py-16">
    <div className="flex flex-col items-center gap-3">
      <div className="w-6 h-6 border-2 border-gray-300 border-t-blue-500 rounded-full animate-spin" />
      <p className="text-sm text-gray-400">Loading classes...</p>
    </div>
  </div>
);

const EmptyState = (props: EmptyStateProps) => (
  <div className="flex flex-col justify-center items-center w-full py-16 gap-2">
    <p className="text-sm font-medium text-gray-500">{props.msg}</p>
    <p className="text-xs text-gray-400">{props.description}</p>
  </div>
);

const ShowClasses = ({ SchoolId }: ShowClassesProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { classes, fetchState } = useClasses(SchoolId);

  const onDelete = async (id: string) => {
    if (window.confirm("Are you sure you want to delete this class?")) {
      await api.delete(`classes/${id}`); // first have to write this api in the backend then i'll apply it here.
    }
  };

  const onSubmit = async (data: ClassFormData) => {
    try {
      // Sending data to your backend
      // Note: You might need to include SchoolId here depending on your API
      await api.post("/Class/AddClass", {
        schoolName: data.schoolName,
        classGrade: data.classGrade,
        classSection: data.classSection,
      });

      setIsModalOpen(false);
      reset(); // Clear form
      // refreshClasses(); // Trigger your hook to re-fetch
    } catch (error) {
      console.error("Failed to create class", error);
      toast.error("Failed to Add new Class")
    }
  };

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<ClassFormData>({
    defaultValues: {
      schoolName: "PSM", // Setting your default as requested
    },
  });

  if (fetchState === "loading") return <LoadingState />;
  if (fetchState === "error")
    return <EmptyState msg="Error occurred while fetching the Classes." />;

  return (
    <div className="flex flex-wrap gap-6 p-6">
      {/* Always show Add Card first */}
      <AddClassCard onClick={() => setIsModalOpen(true)} />

      {classes.map((classItem) => (
        <ClassCard
          key={classItem.classId}
          classItem={classItem}
          onDelete={onDelete}
        />
      ))}

      {/* Logic for your Form Modal can go here or in the parent page */}
      {/* --- FORM MODAL --- */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-sm p-4">
          <div className="bg-white rounded-2xl shadow-2xl w-full max-w-md overflow-hidden">
            <div className="px-6 py-4 border-b border-gray-100 flex justify-between items-center">
              <h2 className="text-xl font-bold text-gray-800">Add New Class</h2>
              <button
                onClick={() => setIsModalOpen(false)}
                className="text-gray-400 hover:text-gray-600"
              >
                <X size={20} />
              </button>
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="p-6 space-y-4">
              {/* School Name */}
              <div>
                <label className="block text-xs font-semibold text-gray-500 uppercase mb-1">
                  School Name
                </label>
                <input
                  {...register("schoolName", {
                    required: "School name is required",
                  })}
                  className="w-full px-4 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all"
                  placeholder="e.g. PSM"
                />
                {errors.schoolName && (
                  <p className="text-red-500 text-[10px] mt-1">
                    {errors.schoolName.message}
                  </p>
                )}
              </div>

              <div className="grid grid-cols-2 gap-4">
                {/* Class Grade */}
                <div>
                  <label className="block text-xs font-semibold text-gray-500 uppercase mb-1">
                    Class Grade
                  </label>
                  <input
                    {...register("classGrade", {
                      required: "Grade is required",
                    })}
                    className="w-full px-4 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all"
                    placeholder="e.g. 10"
                  />
                </div>

                {/* Class Section */}
                <div>
                  <label className="block text-xs font-semibold text-gray-500 uppercase mb-1">
                    Section
                  </label>
                  <input
                    {...register("classSection", {
                      required: "Section is required",
                    })}
                    className="w-full px-4 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all"
                    placeholder="e.g. C"
                  />
                </div>
              </div>

              {/* Action Buttons */}
              <div className="flex gap-3 pt-4">
                <button
                  type="button"
                  onClick={() => setIsModalOpen(false)}
                  className="flex-1 px-4 py-2.5 text-sm font-semibold text-gray-600 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="flex-1 px-4 py-2.5 text-sm font-semibold text-white bg-blue-600 hover:bg-blue-700 rounded-lg shadow-md shadow-blue-100 transition-all disabled:opacity-50"
                >
                  {isSubmitting ? "Saving..." : "Create Class"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default ShowClasses;
