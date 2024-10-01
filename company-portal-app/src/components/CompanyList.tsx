import React, { useState, useEffect } from 'react';
import {
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton, TablePagination,
    Dialog, DialogTitle, DialogContent, DialogActions, Button, Snackbar, Alert, TextField, InputAdornment
} from '@mui/material';
import { getAllCompanies, deleteCompany } from '../api/companyApi';
import { CompanyDTO } from '../types/company';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import SearchIcon from '@mui/icons-material/Search';
import ClearIcon from '@mui/icons-material/Clear';
import ExportIcon from '@mui/icons-material/SaveAlt';
import CompanyForm from './CompanyForm';
import * as XLSX from 'xlsx';

const CompanyList: React.FC = () => {
    const [companies, setCompanies] = useState<CompanyDTO[]>([]);
    const [filteredCompanies, setFilteredCompanies] = useState<CompanyDTO[]>([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [openDialog, setOpenDialog] = useState(false);
    const [openDeleteDialog, setOpenDeleteDialog] = useState(false);
    const [companyToDelete, setCompanyToDelete] = useState<number | null>(null);
    const [editingCompanyId, setEditingCompanyId] = useState<number | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null); 
    const [successMessage, setSuccessMessage] = useState<string | null>(null); 

    useEffect(() => {
        loadCompanies();
    }, []);

    useEffect(() => {
        // Filter companies based on the search term
        setFilteredCompanies(
            companies.filter(company =>
                company.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                company.ticker.toLowerCase().includes(searchTerm.toLowerCase()) ||
                company.isin.toLowerCase().includes(searchTerm.toLowerCase()) ||
                company.exchange.toLowerCase().includes(searchTerm.toLowerCase())
            )
        );
    }, [searchTerm, companies]);

    const loadCompanies = async () => {
        try {
            const response = await getAllCompanies();
            setCompanies(response.data);
            setFilteredCompanies(response.data); 
            setErrorMessage(null); 
        } catch (error) {
            setErrorMessage('Error loading companies. Please try again later.');
            console.error('Error loading companies:', error);
        }
    };

    const handleDelete = async () => {
        if (companyToDelete !== null) {
            try {
                await deleteCompany(companyToDelete);
                loadCompanies(); 
                handleCloseDeleteDialog(); 
                setSuccessMessage('Company deleted successfully');
                setErrorMessage(null); 
            } catch (error) {
                setErrorMessage('Error deleting company. Please try again later.'); 
                console.error('Error deleting company:', error);
            }
        }
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleEditClick = (id: number) => {
        setEditingCompanyId(id);
        setOpenDialog(true);
    };

    const handleAddClick = () => {
        setEditingCompanyId(null);
        setOpenDialog(true);
    };

    const handleDialogClose = () => {
        setOpenDialog(false);
        setEditingCompanyId(null);
    };

    const handleFormSuccess = (message: string) => {
        handleDialogClose();
        loadCompanies(); 
        setSuccessMessage(message);
    };

    const handleOpenDeleteDialog = (id: number) => {
        setCompanyToDelete(id);
        setOpenDeleteDialog(true);
    };

    const handleCloseDeleteDialog = () => {
        setOpenDeleteDialog(false);
        setCompanyToDelete(null);
    };

    const handleSnackbarClose = () => {
        setErrorMessage(null);
        setSuccessMessage(null);
    };

    const handleClearSearch = () => {
        setSearchTerm(''); 
    };

    const handleExportToExcel = () => {
        const worksheet = XLSX.utils.json_to_sheet(filteredCompanies);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'Companies');
        
        XLSX.writeFile(workbook, 'companies.xlsx');
    };

    return (
        <Paper>            
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '10px' }}>
                <TextField
                    label="Search"
                    variant="outlined"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)} 
                    style={{ marginRight: '10px', flexGrow: 1 }} 
                    InputProps={{
                        startAdornment: (
                            <InputAdornment position="start">
                                <SearchIcon /> 
                            </InputAdornment>
                        ),
                        endAdornment: (
                            searchTerm && (
                                <InputAdornment position="end">
                                    <IconButton onClick={handleClearSearch}>
                                        <ClearIcon /> 
                                    </IconButton>
                                </InputAdornment>
                            )
                        )
                    }}
                />
                <Button
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon />}
                    onClick={handleAddClick}
                >
                    Add Company
                </Button>
                <Button
                    variant="contained"
                    color="secondary"
                    onClick={handleExportToExcel} 
                    startIcon={<ExportIcon />}
                    style={{ marginLeft: '10px' }}
                >
                    Export to Excel
                </Button>
            </div>

            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Ticker</TableCell>
                            <TableCell>Exchange</TableCell>
                            <TableCell>ISIN</TableCell>
                            <TableCell>Website</TableCell>
                            <TableCell align="right">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredCompanies.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((company) => (
                            <TableRow key={company.id}>
                                <TableCell>{company.name}</TableCell>
                                <TableCell>{company.ticker}</TableCell>
                                <TableCell>{company.exchange}</TableCell>
                                <TableCell>{company.isin}</TableCell>
                                <TableCell>
                                    {company.website ? (
                                        <a href={company.website} target="_blank" rel="noopener noreferrer">
                                            {company.website}
                                        </a>
                                    ) : (
                                        'N/A'
                                    )}
                                </TableCell>
                                <TableCell align="right">
                                    <IconButton edge="end" aria-label="edit" onClick={() => handleEditClick(company.id!)} >
                                        <EditIcon />
                                    </IconButton>
                                    <IconButton edge="end" aria-label="delete" onClick={() => handleOpenDeleteDialog(company.id!)} >
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            {/* Display total record count */}
            <div style={{ padding: '10px' }}>
                Total Records: {filteredCompanies.length}
            </div>

            <TablePagination
                component="div"
                count={filteredCompanies.length}
                page={page}
                onPageChange={handleChangePage}
                rowsPerPage={rowsPerPage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                rowsPerPageOptions={[5, 10, 25]}
                labelRowsPerPage="Rows per page"
                showFirstButton
                showLastButton
            />

            {/* Dialog for adding/editing company */}
            <Dialog open={openDialog} onClose={handleDialogClose} maxWidth="md" fullWidth>
                <DialogTitle>{editingCompanyId ? "Edit Company" : "Add Company"}</DialogTitle>
                <DialogContent>
                    <CompanyForm companyId={editingCompanyId} onSuccess={() => handleFormSuccess(editingCompanyId ? 'Company updated successfully' : 'Company added successfully')} />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDialogClose} color="primary">
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>

            {/* Confirmation Dialog for deleting a company */}
            <Dialog open={openDeleteDialog} onClose={handleCloseDeleteDialog} maxWidth="sm" fullWidth>
                <DialogTitle>Delete Company</DialogTitle>
                <DialogContent>
                    Are you sure you want to delete this company?
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseDeleteDialog} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleDelete} color="secondary">
                        Confirm Delete
                    </Button>
                </DialogActions>
            </Dialog>

            {/* Snackbar for displaying success and error messages */}
            <Snackbar open={!!errorMessage || !!successMessage} autoHideDuration={6000} onClose={handleSnackbarClose}>
                {errorMessage ? (
                    <Alert onClose={handleSnackbarClose} severity="error" sx={{ width: '100%' }}>
                        {errorMessage}
                    </Alert>
                ) : (
                    <Alert onClose={handleSnackbarClose} severity="success" sx={{ width: '100%' }}>
                        {successMessage}
                    </Alert>
                )}
            </Snackbar>
        </Paper>
    );
};

export default CompanyList;
