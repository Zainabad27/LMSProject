import { useEffect, useState } from "react";
import { toast } from "../Toast";
import api from "../AxiosConfig";
import type { PaginatedTeachers } from "../Dtos/GetTeachers";

type FetchState = "idle" | "loading" | "success" | "error";

const useTeachers = (schoolId: string) => {
  const [Teachers, SetTeachers] = useState<PaginatedTeachers>();
  const [fetchState, setFetchState] = useState<FetchState>("idle");

  useEffect(() => {
    if (!schoolId) return;

    const fetchCourses = async () => {
      setFetchState("loading");
      try {
        const { data } = await api.get<PaginatedTeachers>(
          `/Schools/GetAllTeachers`,
        );
        SetTeachers(data);
        setFetchState("success");
      } catch (error: any) {
        setFetchState("error");
        toast.error(
          error.response?.data?.message ||
            "An error occurred while fetching teachers.",
        );
      }
    };

    fetchCourses();
  }, []);

  return { Teachers, fetchState };
};

export default useTeachers;
