
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { PlusCircle,  UserPlus } from 'lucide-react';
import { employeeSchema, type EmployeeFormValues } from '../ZodSchemas/Schemas';

const ManageEmployees = () => {
  // Add Employee Form
  const { register: regEmployee, handleSubmit: handleEmpSubmit, formState: { errors: empErrors } } = useForm<EmployeeFormValues>({
    resolver: zodResolver(employeeSchema),
  });

  const onAddEmployee = (data: EmployeeFormValues) => console.log("Adding Employee:", data);

  return (
    <div className="p-6 bg-slate-50 min-h-screen space-y-8">
      <header className="border-b pb-4">
        <h1 className="text-2xl font-bold text-slate-800">Employee Management</h1>
        <p className="text-slate-500">Register staff and manage academic assignments.</p>
      </header>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        
        {/* Section: Add Employee */}
        <div className="bg-white p-6 rounded-xl shadow-sm border border-slate-200">
          <div className="flex items-center gap-2 mb-6">
            <UserPlus className="text-blue-600" size={20} />
            <h2 className="text-lg font-semibold">Add New Employee</h2>
          </div>

          <form onSubmit={handleEmpSubmit(onAddEmployee)} className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-slate-700">Full Name</label>
              <input 
                {...regEmployee("name")}
                className="mt-1 w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 outline-none"
                placeholder="John Doe"
              />
              {empErrors.name && <p className="text-red-500 text-xs mt-1">{empErrors.name.message}</p>}
            </div>

            <div>
              <label className="block text-sm font-medium text-slate-700">Email</label>
              <input 
                {...regEmployee("email")}
                className="mt-1 w-full p-2 border rounded-md focus:ring-2 focus:ring-blue-500 outline-none"
              />
              {empErrors.email && <p className="text-red-500 text-xs mt-1">{empErrors.email.message}</p>}
            </div>

            <div>
              <label className="block text-sm font-medium text-slate-700">Designation</label>
              <select 
                {...regEmployee("designation")}
                className="mt-1 w-full p-2 border rounded-md bg-white focus:ring-2 focus:ring-blue-500 outline-none"
              >
                <option value="">Select Role</option>
                <option value="Admin">Admin</option>
                <option value="Teacher">Teacher</option>
              </select>
              {empErrors.designation && <p className="text-red-500 text-xs mt-1">{empErrors.designation.message}</p>}
            </div>

            <button type="submit" className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 rounded-md transition-colors flex items-center justify-center gap-2">
              <PlusCircle size={18} /> Register Employee
            </button>
          </form>
        </div>

        {/* Section: Assign Class to Teacher */}
      

      </div>
    </div>
  );
};

export default ManageEmployees;