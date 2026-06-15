import Card from '@mui/material/Card'
import CardContent from '@mui/material/CardContent'
import Typography from '@mui/material/Typography'

interface Props {
  total: number
}

const ItbisSummaryCard = ({ total }: Props) => {
  return (
    <Card sx={{ maxWidth: 300, mt: 2 }}>
      <CardContent>
        <Typography variant="subtitle2" color="text.secondary">
          Total ITBIS
        </Typography>
        <Typography variant="h5" fontWeight="bold" color="primary">
          RD$ {total.toFixed(2)}
        </Typography>
      </CardContent>
    </Card>
  );
}

export default ItbisSummaryCard;
