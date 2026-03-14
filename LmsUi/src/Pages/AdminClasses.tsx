import ShowClasses from "../Components/DashboardPagesComponents/ShowClasses";

const AdminClassesPage = () => {


  return (
    <div className="p-6">
      <div className="flex flex-wrap gap-6">
        {/* Existing Classes Components */}
        <ShowClasses SchoolId="a14afb86-1e22-4daf-b94c-b70a70e77421" />
      </div>
    </div>
  );
};

export default AdminClassesPage;
