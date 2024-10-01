import React from 'react';
import { useNavigate } from 'react-router-dom';
import CompanyList from '../components/CompanyList';

const CompanyPage: React.FC = () => {
    const navigate = useNavigate();
    
    return (
        <div>                        
            <CompanyList />
        </div>
    );
};

export default CompanyPage;
