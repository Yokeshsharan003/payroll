import React, { useEffect } from "react";
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from "react-router-dom";
import ProtectedRoute from './ProtectedRoute/ProtectedRoute';
import Login from './Pages/Login';
import LandingPage from './Pages/LandingPage';
import Admin from './Admin/Admin';
import ManageUser from './Admin/ManageUser';
import PayrollPolicies from './Admin/PayrollPolicies';
import ComplianceReports from './Admin/ComplianceReports';
import ManageEmployee from './Admin/ManageEmployee';
import Employee from './Employee/Employee';
import Home from './Employee/Home';
import Profile from './Employee/Profile';
import RequestLeave from './Employee/RequestLeave';
import ViewPaystubs from './Employee/ViewPaystubs';
import Processor from './Processor/Processor';
import CalculatePayroll from './Processor/CalculatePayroll';
import ManageBenefits from './Processor/ManageBenefits';
import Manager from './Manager/Manager';
import LeaveRequest from './Manager/LeaveRequest';
import EmailVerification from './Pages/EmailVerification';
import CodeCheck from './Pages/CodeCheck';
import ChangePassword from './Pages/ChangePassword';

const App = () => {
  return (
    <Router>
      <ClearLocalStorageOnBack />
      <Routes>
        {/* Public Routes */}
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/EmailVerification" element={<EmailVerification />} />
        <Route path="/codecheck" element={<CodeCheck />} />
        <Route path="/changePassword" element={<ChangePassword />} />

        {/* Admin Routes */}
        <Route
          path="/admin"
          element={
            <ProtectedRoute role="Admin">
              <Admin />
            </ProtectedRoute>
          }
        />
        <Route
          path="/manage-user"
          element={
            <ProtectedRoute role="Admin">
              <ManageUser />
            </ProtectedRoute>
          }
        />
        <Route
          path="/payroll-policies"
          element={
            <ProtectedRoute role="Admin">
              <PayrollPolicies />
            </ProtectedRoute>
          }
        />
        <Route
          path="/compliance-reports"
          element={
            <ProtectedRoute role="Admin">
              <ComplianceReports />
            </ProtectedRoute>
          }
        />
        <Route
          path="/manage-employee"
          element={
            <ProtectedRoute role="Admin">
              <ManageEmployee />
            </ProtectedRoute>
          }
        />

        {/* Employee Routes */}
        <Route
          path="/employee"
          element={
            <ProtectedRoute role="Employee">
              <Employee />
            </ProtectedRoute>
          }
        />
        <Route
          path="/home"
          element={
            <ProtectedRoute role="Employee">
              <Home />
            </ProtectedRoute>
          }
        />
        <Route
          path="/profile"
          element={
            <ProtectedRoute role="Employee">
              <Profile />
            </ProtectedRoute>
          }
        />
        <Route
          path="/request-leave"
          element={
            <ProtectedRoute role="Employee">
              <RequestLeave />
            </ProtectedRoute>
          }
        />
        <Route
          path="/view-paystubs"
          element={
            <ProtectedRoute role="Employee">
              <ViewPaystubs />
            </ProtectedRoute>
          }
        />

        {/* Processor Routes */}
        <Route
          path="/processor"
          element={
            <ProtectedRoute role="PayrollProcessor">
              <Processor />
            </ProtectedRoute>
          }
        />
        <Route
          path="/calculate-payroll"
          element={
            <ProtectedRoute role="PayrollProcessor">
              <CalculatePayroll />
            </ProtectedRoute>
          }
        />
        <Route
          path="/manage-benefits"
          element={
            <ProtectedRoute role="PayrollProcessor">
              <ManageBenefits />
            </ProtectedRoute>
          }
        />

        {/* Manager Routes */}
        <Route
          path="/manager"
          element={
            <ProtectedRoute role="Manager">
              <Manager />
            </ProtectedRoute>
          }
        />
        <Route
          path="/manager-leave"
          element={
            <ProtectedRoute role="Manager">
              <LeaveRequest />
            </ProtectedRoute>
          }
        />

        {/* Redirect unknown routes to landing page */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
};

const ClearLocalStorageOnBack = () => {
  const location = useLocation();

  useEffect(() => {
    if (location.pathname === "/") {
      localStorage.clear();
      console.log("Local storage cleared because you're on the login page.");
    }
  }, [location]);

  return null; // This component does not render anything
};

export default App;
