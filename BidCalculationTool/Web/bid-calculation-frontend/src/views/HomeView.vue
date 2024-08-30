<template>
    <div>
        <h1>Bid Calculator</h1>
        <form @submit.prevent>
            <label for="vehiclePrice">Vehicle Price:</label>
            <input v-model.number="vehiclePrice" type="number" placeholder="Enter vehicle price" required />

            <label for="vehicleType">Vehicle Type:</label>
            <select v-model.number="vehicleType" required>
                <option :value="0">Common</option>
                <option :value="1">Luxury</option>
            </select>

            <button @click="calculate">Calculate</button>

        </form>

        <div v-if="result">
            <h2>Calculation Result</h2>
            <p><strong>Basic Fee:</strong> ${{ result.fees.basic }}</p>
            <p><strong>Special Fee:</strong> ${{ result.fees.special }}</p>
            <p><strong>Association Fee:</strong> ${{ result.fees.association }}</p>
            <p><strong>Storage Fee:</strong> ${{ result.fees.storage }}</p>
            <p><strong>Total Cost:</strong> ${{ result.totalCost }}</p>
        </div>

        <div v-if="errors && errors.length">
            <h2>Error</h2>
            <ul>
                <li v-for="(error, index) in errors" :key="index">{{ error }}</li>
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';
    import api from '../services/api';

    export default defineComponent({
        name: 'HomeView',
        setup() {
            const vehiclePrice = ref<number>(0);
            const vehicleType = ref<number>(0);
            const result = ref<any>(null);
            const errors = ref<string | null>(null);

            const calculate = async () => {
                try {
                    const response = await api.get('/bidcalculation', {
                        params: {
                            vehiclePrice: vehiclePrice.value,
                            vehicleType: vehicleType.value,
                        },
                    });
                    result.value = response.data;
                    errors.value = null;
                } catch (err: any) {
                    if (err.response && err.response.data && err.response.data.errors) {
                        errors.value = Object.values(err.response.data.errors).flat();
                    } else {
                        errors.value = ['An error occurred while processing your request.'];
                    }
                    result.value = null;
                }
            };

            return {
                vehiclePrice,
                vehicleType,
                result,
                calculate,
                errors
            };
        },
    });
</script>