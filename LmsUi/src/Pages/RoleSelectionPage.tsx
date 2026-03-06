import TeacherLoginBox from "../Components/LoginBox";


function RoleSelectionPage() {
  return (
    // This container handles the centering for everything inside it
    <div className="flex h-screen w-full items-center justify-center bg-gray-100">
      {/* If you want the "I am Centered" text AND the login box together: */}
      <div className="flex flex-col items-center gap-4">
        {/* Your actual Component is now also centered! */}
        <TeacherLoginBox LoginFor="Teacher"/>
      </div>
    </div>
  );
}

export default RoleSelectionPage;
