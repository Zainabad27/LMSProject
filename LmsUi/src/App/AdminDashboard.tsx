import { BookOpen, Users, Settings, School, BookCopy, PanelRightOpenIcon } from "lucide-react";
import DialogBox from "../Components/DashboardComponents/DialogBox";

const Dashboard = async () => {
  return (
    <div className="p-8 bg-slate-50 min-h-screen">
      <h1 className="text-2xl font-bold text-slate-900 mb-6">Admin Overview</h1>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {/* <DialogBox
          title="Assignment Box"
          destination="/admin/assignments"
          description="View and grade student submissions"
          icon={<BookOpen size={24} />}
        /> */}
        <DialogBox
          title="User Management"
          destination="/admin/users"
          description="Control permissions and roles"
          icon={<Users size={24} />}
        />
        <DialogBox
          title="Site Settings"
          destination="/admin/settings"
          icon={<Settings size={24} />}
        />
        <DialogBox
          title="Classes"
          destination="/admin/Classes"
          description="Manage and Assign Classes to Faculty"
          icon={<School size={24} />}
        />{" "}
        <DialogBox
          title="Courses"
          destination="/admin/Courses"
          description="Add/Delete Course or Assign Courses to the faculty"
          icon={<BookCopy size={24} />}
        />{" "}
        <DialogBox
          title="Employee"
          destination="/admin/Employee"
          description="Manage the Employees of your School"
          icon={<PanelRightOpenIcon size={24} />}
        />
      </div>
    </div>
  );
};

export default Dashboard;
