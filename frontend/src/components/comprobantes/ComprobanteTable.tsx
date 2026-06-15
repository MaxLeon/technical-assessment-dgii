'use client'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import { ComprobanteFiscal } from '@/lib/types'

interface Props {
  data: ComprobanteFiscal[]
  loading?: boolean
}

const columns: GridColDef<ComprobanteFiscal>[] = [
  { field: 'ncf', headerName: 'NCF', flex: 2 },
  {
    field: 'monto',
    headerName: 'Monto (RD$)',
    flex: 1,
    type: 'number',
    valueFormatter: (value: number) => value.toFixed(2),
  },
  {
    field: 'itbis18',
    headerName: 'ITBIS 18%',
    flex: 1,
    type: 'number',
    valueFormatter: (value: number) => value.toFixed(2),
  },
]

const ComprobanteTable = ({ data, loading = false }: Props) => {
  return (
    <DataGrid
      rows={data}
      columns={columns}
      getRowId={(row) => row.id}
      loading={loading}
      autoHeight
    />
  )
}

export default ComprobanteTable
