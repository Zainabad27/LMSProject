import { useEffect, useState } from "react";
import type { ClassDto } from "../Dtos/GetClasses";
import { toast } from "../Toast";
import api from "../AxiosConfig";

type FetchState = "idle" | "loading" | "success" | "error";

const useClasses = (courseId: string) => {
  const [classes, setClasses] = useState<ClassDto[]>([]);
  const [fetchState, setFetchState] = useState<FetchState>("idle");

  useEffect(() => {
    if (!courseId) return;

    const fetchClasses = async () => {
      setFetchState("loading");
      try {
        const { data } = await api.get<ClassDto[]>(
          `/Class/GetAllClasses/${courseId}`,
        );
        setClasses(data);
        setFetchState("success");
      } catch (error: any) {
        setFetchState("error");
        toast.error(
          error.response?.data?.message ||
            "An error occurred while fetching classes.",
        );
      }
    };

    fetchClasses();
  }, [courseId]);

  return { classes, fetchState };
};

export default useClasses;
