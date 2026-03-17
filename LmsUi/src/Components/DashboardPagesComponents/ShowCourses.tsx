import useCourses from "../../Hooks/UseCourses";
import { useState } from "react";
import api from "../../AxiosConfig";
import AddCourseCard from "./AddItemCard";
import { useForm } from "react-hook-form";
import { X } from "lucide-react";
import { toast } from "../../Toast";
import CourseCard from "./CourseCard";
import type { CourseFormData } from "../../ZodSchemas/AddCourseForm";

interface ShowCoursesProps {
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

const ShowCourses = ({ SchoolId }: ShowCoursesProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { courses, fetchState } = useCourses(SchoolId);

  const onDelete = async (id: string) => {
    if (window.confirm("Are you sure you want to delete this Course?")) {
     try {
       await api.delete(`Class/Course/Delete/${id}`);
       toast.success("Course Deleted Successfully.");
      
     } catch (error) {
      console.error("Could not delete the Course",error);
      toast.error("Could not Delete the Course Right now.");
      
     }
    }
  };

  const onSubmit = async (data: CourseFormData) => {
    try {
      // Sending data to your backend
      // Note: You might need to include SchoolId here depending on your API
      await api.post("/Class/AddCourse", {
        CourseName: data.CourseName,
        BoardOrDepartment: data.BoardOrDepartment,
      });

      setIsModalOpen(false);
      reset(); // Clear form
      // refreshClasses(); // Trigger your hook to re-fetch
    } catch (error) {
      console.error("Failed to create Course", error);
      toast.error("Failed to Add new Course");
    }
  };

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<CourseFormData>();

  if (fetchState === "loading") return <LoadingState />;
  if (fetchState === "error")
    return <EmptyState msg="Error occurred while fetching the Courses." />;

  return (
    <div className="flex flex-wrap gap-6 p-6">
      {/* Always show Add Card first */}
      <AddCourseCard
        onClick={() => setIsModalOpen(true)}
        text="Add a new Course"
      />

      {courses.map((courseItem) => (
        <CourseCard
          key={courseItem.courseId}
          CourseItem={courseItem}
          onDelete={onDelete}
        />
      ))}

      {/* Logic for your Form Modal can go here or in the parent page */}
      {/* --- FORM MODAL --- */}
      {isModalOpen && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-sm p-4">
          <div className="bg-white rounded-2xl shadow-2xl w-full max-w-md overflow-hidden">
            <div className="px-6 py-4 border-b border-gray-100 flex justify-between items-center">
              <h2 className="text-xl font-bold text-gray-800">Add New Course</h2>
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
                  Course Name
                </label>
                <input
                  {...register("CourseName", {
                    required: "Course name is required",
                  })}
                  className="w-full px-4 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all"
                  placeholder="e.g. Calculus"
                />
                {errors.CourseName && (
                  <p className="text-red-500 text-[10px] mt-1">
                    {errors.CourseName.message}
                  </p>
                )}
              </div>

              <div className="grid grid-cols-2 gap-4">
                {/* Class Grade */}
                <div>
                  <label className="block text-xs font-semibold text-gray-500 uppercase mb-1">
                    Board Or Department
                  </label>
                  <input
                    {...register("BoardOrDepartment", {
                      required: "Board is essential for adding a new course",
                    })}
                    className="w-full px-4 py-2 bg-gray-50 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition-all"
                    placeholder="e.g. Sindh"
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
                  {isSubmitting ? "Saving..." : "Create Course"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default ShowCourses;
