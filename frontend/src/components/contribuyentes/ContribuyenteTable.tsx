'use client'
import { DataGrid, GridColDef, GridRowParams } from '@mui/x-data-grid'
import Chip from '@mui/material/Chip'
import { useRouter } from 'next/navigation'
import { Contribuyente } from '@/lib/types'

interface Props {
  data: Contribuyente[]
  loading?: boolean
}

const columns: GridColDef<Contribuyente>[] = [
  { field: 'rncCedula', headerName: 'RNC / Cédula', flex: 1 },
  { field: 'nombre', headerName: 'Nombre', flex: 2 },
  { field: 'tipo', headerName: 'Tipo', flex: 1 },
  {
    field: 'estatus',
    headerName: 'Estatus',
    flex: 1,
    renderCell: ({ value }) => (
      <Chip
        label={value}
        color={value === 'activo' ? 'success' : 'default'}
        size="small"
      />
    ),
  },
]

const ContribuyenteTable = ({ data, loading = false }: Props) => {
  const router = useRouter()

  const handleRowClick = (params: GridRowParams<Contribuyente>) => {
    router.push(`/contribuyentes/${params.row.rncCedula}`)
  }

  return (
    <DataGrid
      rows={data}
      columns={columns}
      getRowId={(row) => row.rncCedula}
      onRowClick={handleRowClick}
      loading={loading}
      autoHeight
      sx={{ cursor: 'pointer' }}
    />
  )
}

export default ContribuyenteTable;
