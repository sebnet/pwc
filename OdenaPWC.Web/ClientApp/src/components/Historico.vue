<template>
    <h1 id="tableLabel">Histórico de alertas</h1>

    <p>Revise las alertas del servicio históricas registradas</p>
    <div>
        Linea: <input v-model="inputData.linea" placeholder="Linea">
        Desde: <input v-model="inputData.from" placeholder="DD/MM/AA">
        Hasta: <input v-model="inputData.to" placeholder="DD/MM/AA">
        <button v-on:click="getHistorico">Buscar</button>
    </div>


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
    <p v-if="alerts && alerts.length==0"><em>No se han encontrado resultados</em></p>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "Historico",
        data() {
            return {
                alerts: null,
                inputData: {
                    from: '',
                    to: '',
                    linea: ''
                }
            }
        },
        computed: {
            url() {
                return 'transport/GetHistorico/linea/' + this.inputData.linea + '?from=' + this.inputData.from + '&to=' + this.inputData.to;
            }
        },
        methods: {
            getHistorico() {
                axios.get(this.url)
                    .then((response) => {
                        this.alerts = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        }
    }
</script>