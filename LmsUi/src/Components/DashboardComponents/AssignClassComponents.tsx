import { zodResolver } from "@hookform/resolvers/zod";
import { BookOpen } from "lucide-react";
import { assignClassSchema, type AssignClassFormValues } from "../../ZodSchemas/AssignClassSchema";
import { useForm } from "react-hook-form";

const AssignClassComponent=()=>{
    

     const { register: regAssign, handleSubmit: handleAssignSubmit, formState: { errors: assignErrors } } = useForm<AssignClassFormValues>({
        resolver: zodResolver(assignClassSchema),
      });
    const onAssignClass=()=>{
        // to be implemented
    }
    <>
      <div className="bg-white p-6 rounded-xl shadow-sm border border-slate-200">
              <div className="flex items-center gap-2 mb-6">
                <BookOpen className="text-indigo-600" size={20} />
                <h2 className="text-lg font-semibold">Assign Class</h2>
              </div>
    
              <form onSubmit={handleAssignSubmit(onAssignClass)} className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-slate-700">Select Teacher</label>
                  <select 
                    {...regAssign("teacherId")}
                    className="mt-1 w-full p-2 border rounded-md bg-white focus:ring-2 focus:ring-indigo-500 outline-none"
                  >
                    <option value="">Choose a teacher...</option>
                    {/* Map your actual teacher data here */}
                    <option value="t1">Mr. Smith (ID: 101)</option>
                    <option value="t2">Ms. Davis (ID: 102)</option>
                  </select>
                  {/* {empErrors.designation && <p className="text-red-500 text-xs mt-1">{assignErrors.teacherId?.message}</p>} */}
                </div>
    
                <div>
                  <label className="block text-sm font-medium text-slate-700">Select Class</label>
                  <select 
                    {...regAssign("classId")}
                    className="mt-1 w-full p-2 border rounded-md bg-white focus:ring-2 focus:ring-indigo-500 outline-none"
                  >
                    <option value="">Choose a class...</option>
                    <option value="c1">Mathematics - Grade 10</option>
                    <option value="c2">Physics - Grade 12</option>
                  </select>
                  {assignErrors.classId && <p className="text-red-500 text-xs mt-1">{assignErrors.classId.message}</p>}
                </div>
    
                <div className="p-4 bg-indigo-50 rounded-lg text-xs text-indigo-700">
                  Only employees registered as <strong>Teachers</strong> will appear in the selection list above.
                </div>
    
                <button type="submit" className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-2 rounded-md transition-colors">
                  Confirm Assignment
                </button>
              </form>
            </div></>

}


export default AssignClassComponent;

function handleAssignSubmit(onAssignClass: any): import("react").SubmitEventHandler<HTMLFormElement> | undefined {
    throw new Error("Function not implemented.");
}
