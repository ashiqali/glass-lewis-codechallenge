import React, { useState, useEffect } from 'react';
import {
    TextField,
    Button,
    Snackbar,
    Alert,
} from '@mui/material';
import { CompanyDTO } from '../types/company';
import { createCompany, getCompanyById, updateCompany } from '../api/companyApi';
import axios from 'axios';

interface CompanyFormProps {
    companyId: number | null;
    onSuccess: () => void;
}

const CompanyForm: React.FC<CompanyFormProps> = ({ companyId, onSuccess }) => {
    const [company, setCompany] = useState<CompanyDTO>({
        id: 0,
        name: '',
        ticker: '',
        exchange: '',
        isin: '',
        website: '',
    });
    const [errorMessage, setErrorMessage] = useState<string | null>(null); 
    const [loading, setLoading] = useState<boolean>(false); 

    useEffect(() => {
        if (companyId) {
            loadCompany();
        }
    }, [companyId]);

    const loadCompany = async () => {
        try {
            const response = await getCompanyById(companyId!);
            setCompany(response.data);
            setErrorMessage(null); 
        } catch (error) {
            setErrorMessage('Error loading company details.'); 
            console.error('Error loading company:', error);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setCompany({ ...company, [name]: value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true); 
        try {
            if (companyId) {
                await updateCompany(companyId, company);
            } else {
                await createCompany(company);
            }
            onSuccess();
            setErrorMessage(null); 
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                setErrorMessage(`Error submitting company details: ${error.response.data}`);
            } else {
                setErrorMessage('Error submitting company details. Please try again later.');
            }
            console.error('Error submitting company details:', error);
        } finally {
            setLoading(false); 
        }
    };

    const handleSnackbarClose = () => {
        setErrorMessage(null); 
    };

    return (
        <>
            <form onSubmit={handleSubmit}>
                <TextField
                    name="name"
                    label="Company Name"
                    value={company.name}
                    onChange={handleChange}
                    required
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="ticker"
                    label="Ticker"
                    value={company.ticker}
                    onChange={handleChange}
                    required
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="exchange"
                    label="Exchange"
                    value={company.exchange}
                    onChange={handleChange}
                    required
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="isin"
                    label="ISIN"
                    value={company.isin}
                    onChange={handleChange}
                    required
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="website"
                    label="Website"
                    value={company.website}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                />
                <Button type="submit" variant="contained" color="primary" style={{ marginTop: '16px' }} disabled={loading}>
                     {companyId ? 'Update Company' : 'Add Company'}
                </Button>
            </form>

            {/* Snackbar for displaying error messages */}
            <Snackbar open={!!errorMessage} autoHideDuration={6000} onClose={handleSnackbarClose}>
                <Alert onClose={handleSnackbarClose} severity="error" sx={{ width: '100%' }}>
                    {errorMessage}
                </Alert>
            </Snackbar>
        </>
    );
};

export default CompanyForm;
