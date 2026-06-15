export const dynamic = 'force-dynamic'

import Container from '@mui/material/Container'
import Typography from '@mui/material/Typography'
import Box from '@mui/material/Box'
import Button from '@mui/material/Button'
import ArrowBackIcon from '@mui/icons-material/ArrowBack'
import Link from 'next/link'
import { contribuyentesApi, comprobantesApi } from '@/lib/api'
import ComprobanteTable from '@/components/comprobantes/ComprobanteTable'
import ItbisSummaryCard from '@/components/comprobantes/ItbisSummaryCard'

interface Props {
  params: { rnc: string }
}

export default async function ContribuyenteDetallePage({ params }: Props) {
  const [contribuyente, comprobantes, totalItbis] = await Promise.all([
    contribuyentesApi.GetByRncCedula(params.rnc),
    comprobantesApi.GetByRncCedula(params.rnc),
    comprobantesApi.GetTotalItbis(params.rnc),
  ])

  return (
    <Container maxWidth="lg">
      <Box py={4}>
        <Button
          component={Link}
          href="/contribuyentes"
          startIcon={<ArrowBackIcon />}
          sx={{ mb: 2 }}
        >
          Volver
        </Button>

        <Typography variant="h4" fontWeight="bold" gutterBottom>
          {contribuyente.nombre}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary" gutterBottom>
          {contribuyente.tipo} · RNC/Cédula: {contribuyente.rncCedula}
        </Typography>

        <Typography variant="h6" mt={3} mb={1}>
          Comprobantes Fiscales
        </Typography>

        <ComprobanteTable data={comprobantes} />
        <ItbisSummaryCard total={totalItbis.totalItbis} />
      </Box>
    </Container>
  )
}
