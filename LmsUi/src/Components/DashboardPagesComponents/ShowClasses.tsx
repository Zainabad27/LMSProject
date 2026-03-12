import { useEffect, useState } from "react";
import api from "../../AxiosConfig";
import type { ClassDto } from "../../Dtos/GetClasses";
import { toast } from "../../Toast";
import ClassCard from "./ClassesCard";

// ─── Types ────────────────────────────────────────────────────────────────────

interface ShowClassesProps {
  courseId: string;
}

type FetchState = "idle" | "loading" | "success" | "error";

// ─── Custom Hook ──────────────────────────────────────────────────────────────

const useClasses = (courseId: string) => {
  const [classes, setClasses] = useState<ClassDto[]>([]);
  const [fetchState, setFetchState] = useState<FetchState>("idle");

  useEffect(() => {
    if (!courseId) return;

    const fetchClasses = async () => {
      setFetchState("loading");
      try {
        const { data } = await api.get<ClassDto[]>(
          `/Class/GetAllClasses/${courseId}`
        );
        setClasses(data);
        setFetchState("success");
      } catch (error: any) {
        setFetchState("error");
        toast.error(
          error.response?.data?.message ??
            "An error occurred while fetching classes."
        );
      }
    };

    fetchClasses();
  }, [courseId]);

  return { classes, fetchState };
};

// ─── Sub-components ───────────────────────────────────────────────────────────



const LoadingState = () => (
  <div className="flex justify-center items-center w-full py-16">
    <div className="flex flex-col items-center gap-3">
      <div className="w-6 h-6 border-2 border-gray-300 border-t-blue-500 rounded-full animate-spin" />
      <p className="text-sm text-gray-400">Loading classes...</p>
    </div>
  </div>
);

const EmptyState = () => (
  <div className="flex flex-col justify-center items-center w-full py-16 gap-2">
    <p className="text-sm font-medium text-gray-500">No classes found</p>
    <p className="text-xs text-gray-400">
      There are no classes available for this course.
    </p>
  </div>
);

// ─── Main Component ───────────────────────────────────────────────────────────

const ShowClasses = ({ courseId }: ShowClassesProps) => {
  const { classes, fetchState } = useClasses(courseId);

  if (fetchState === "loading") return <LoadingState />;
  if (fetchState === "success" && classes.length === 0) return <EmptyState />;

  return (
    <div className="flex flex-wrap gap-4 p-5">
      {classes.map((classItem) => (
        <ClassCard key={classItem.classId} classItem={classItem} />
      ))}
    </div>
  );
};

export default ShowClasses;