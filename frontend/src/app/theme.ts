'use client'
import { createTheme } from '@mui/material/styles'

const theme = createTheme({
  palette: {
    primary: { main: '#1565c0' },
    secondary: { main: '#f57c00' },
  },
  typography: {
    fontFamily: 'Inter, system-ui, sans-serif',
  },
})

export default theme
