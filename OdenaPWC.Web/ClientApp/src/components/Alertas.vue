<template>
    <h1 id="tableLabel">Alertas de servicio de subtes</h1>

    <p>Alertas vigentes de interrupción de servicio de subtes en Bs. As</p>

    <p v-if="!alerts"><em>Loading...</em></p>

    <table class='table table-striped' aria-labelledby="tableLabel" v-if="alerts">
        <thead>
            <tr>
                <th>Fecha</th>
                <th>Linea</th>
                <th>Alerta</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="alert of alerts" v-bind:key="alert">
                <td>{{ alert.timeStamp }}</td>
                <td>{{ alert.line }}</td>
                <td>{{ alert.message }}</td>
            </tr>
        </tbody>
    </table>
    <p v-if="alerts && alerts.length==0"><em>No se han encontrado nuevas alertas de servicio</em></p>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "Alertas",
        data() {
            return {
                alerts: null
            }
        },
        methods: {
            getAlerts() {
                axios.get('transport/GetAlerts')
                    .then((response) => {
                        this.alerts =  response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getAlerts();
        }
    }
</script>