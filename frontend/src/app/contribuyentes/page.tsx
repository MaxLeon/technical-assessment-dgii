export const dynamic = 'force-dynamic'

import Container from '@mui/material/Container'
import Typography from '@mui/material/Typography'
import Box from '@mui/material/Box'
import { contribuyentesApi } from '@/lib/api'
import ContribuyenteTable from '@/components/contribuyentes/ContribuyenteTable'

export default async function ContribuyentesPage() {
  const contribuyentes = await contribuyentesApi.GetContribuyentes();
  return (
    <Container maxWidth="lg">
      <Box py={4}>
        <Typography variant="h4" fontWeight="bold" gutterBottom>
          Contribuyentes
        </Typography>
        <ContribuyenteTable data={contribuyentes} />
      </Box>
    </Container>
  )
}
