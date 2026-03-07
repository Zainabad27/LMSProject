import TeacherLoginBox from "../Components/LoginBox";


function RoleSelectionPage() {
  return (
    // This container handles the centering for everything inside it
    <div className="flex h-screen w-full items-center justify-center bg-gray-100">
      {/* If you want the "I am Centered" text AND the login box together: */}
    <div className="grid grid-cols-2 gap-6 max-w-4xl mx-auto items-center justify-items-center">
  {/* First Row */}
  <TeacherLoginBox LoginFor="Teacher" />
  <TeacherLoginBox LoginFor="Student" />

  {/* Second Row - Centered across both columns */}
  <div className="col-span-2">
    <TeacherLoginBox LoginFor="Admin" />
  </div>
</div>
    </div>
  );
}

export default RoleSelectionPage;
