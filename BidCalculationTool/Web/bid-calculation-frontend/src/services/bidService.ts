import api from './api';

export const calculateBid = async (vehiclePrice: number, vehicleType: string) => {
  try {
    const response = await api.post('/bidcalculation', { vehiclePrice, vehicleType });
    return response.data;
  } catch (error) {
    console.error('Error calculating bid:', error);
    throw error;
  }
};
