<template>
    <h1 id="tableLabel">Horarios de llegadas de trenes</h1>

    <p>Consulte el horario de llegada del próximo tren a cualquier estación de la red de subtes</p>

    <div>
        Linea: <select v-model="SelectedLine">
            <option v-for="(item,index) in subtes" :key="index" v-bind:value="item.value">
                {{ item.text }}
            </option>
        </select>
        Estacion: <select v-model="SelectedStop">
            <option v-for="(item,index) in stops" :key="index" v-bind:value="item.value">
                {{ item.text }}
            </option>
        </select>
        Destino: <select v-model="SelectedDestination">
            <option v-for="(item,index) in destinations" :key="index" v-bind:value="item.value">
                {{ item.text }}
            </option>
        </select>
        <button v-on:click="getForecast">Buscar</button>


    </div>
    <div v-if="result">
        El próximo tren llegará a las {{result}}
    </div>


</template>


<script>
    import axios from 'axios'
    export default {
        name: "Forecast",
        data() {
            return {
                SelectedLine: '',
                SelectedStop: '',
                SelectedDestination: '',
                lines: [],
                stops: [],
                destinations: [],
                result: null,
                subtes: {
                    LineaA: {
                        text: 'A',
                        value: 'LineaA',
                        cabeceras: [
                            { text: 'San Pedrito', value: '1' },
                            { text: 'Plaza de Mayo', value: '0' }
                        ],
                        stops: [
                            { text: 'San José de Flores', value: '1060' },
                            { text: 'Carabobo', value: '1061' },
                            { text: 'Puán', value: '1062' },
                            { text: 'Primera Junta', value: '1063' },
                            { text: 'Acoyte', value: '1064' },
                            { text: 'Río de Janeiro', value: '1065' },
                            { text: 'Castro Barros', value: '1066' },
                            { text: 'Loria', value: '1067' },
                            { text: 'Plaza Miserere', value: '1068' },
                            { text: 'Pasco', value: '1070' },
                            { text: 'Congreso', value: '1071' },
                            { text: 'Sáenz Peña', value: '1072' },
                            { text: 'Lima', value: '1073' },
                            { text: 'Piedras', value: '1074' },
                            { text: 'Perú', value: '1075' },
                            { text: 'Plaza de Mayo', value: '1076' }
                        ],
                    },
                    LineaB: {
                        text: 'B',
                        value: 'LineaB',
                        cabeceras: [
                            { text: "Juan Manuel de Rosas", value: "1" },
                            { text: "Leandro N. Alem", value: "0" }
                        ],
                        stops: [
                            { text: "Juan Manuel de Rosas", value: "1077" },
                            { text: "Echeverría", value: "1078" },
                            { text: "De Los Incas - Parque Chas", value: "1079" },
                            { text: "Tronador - Villa Ortúzar", value: "1080" },
                            { text: "Federico Lacroze", value: "1081" },
                            { text: "Dorrego", value: "1082" },
                            { text: "Malabia", value: "1084" },
                            { text: "Ángel Gallardo", value: "1083" },
                            { text: "Medrano", value: "1085" },
                            { text: "Carlos Gardel", value: "1086" },
                            { text: "Pueyrredón", value: "1087" },
                            { text: "Pasteur", value: "1088" },
                            { text: "Callao", value: "1089" },
                            { text: "Uruguay", value: "1090" },
                            { text: "Carlos Pelegrini", value: "1091" },
                            { text: "Florida", value: "1092" },
                            { text: "Leandro N. Alem", value: "1093" }
                        ],
                    },
                    LineaD: {
                        text: 'D',
                        value: 'LineaD',
                        cabeceras: [
                            { text: "Congreso de Tucumán", value: "1" },
                            { text: "Catedral", value: "0" },
                        ],
                        stops: [
                            { text: "Congreso de Tucumán", value: "1103" },
                            { text: "Juramento", value: "1104" },
                            { text: "José Hernández", value: "1105" },
                            { text: "Olleros", value: "1106" },
                            { text: "Ministro Carranza", value: "1107" },
                            { text: "Palermo", value: "1108" },
                            { text: "Plaza Italia", value: "1109" },
                            { text: "Scalabrini Ortíz", value: "1110" },
                            { text: "Bulnes", value: "1111" },
                            { text: "Agüero", value: "1112" },
                            { text: "Pueyrredón", value: "1113" },
                            { text: "Facultad de Medicina", value: "1114" },
                            { text: "Callao", value: "1115" },
                            { text: "Tribunales", value: "1116" },
                            { text: "9 de Julio", value: "1117" },
                            { text: "Catedral", value: "1118" },
                        ],
                    },
                    LineaE: {
                        text: 'E',
                        value: 'LineaE',
                        cabeceras: [
                            { text: "Plaza de los Virreyes", value: "1" },
                            { text: "Retiro", value: "0" }
                        ],
                        stops: [
                            { text: "Plaza de los Virreyes", value: "1133" },
                            { text: "Varela", value: "1132" },
                            { text: "Medalla Milagrosa", value: "1131" },
                            { text: "Emilio Mitre", value: "1130" },
                            { text: "Moreno", value: "1129" },
                            { text: "Av. La Plata", value: "1128" },
                            { text: "Boedo", value: "1127" },
                            { text: "General Urquiza", value: "1126" },
                            { text: "Jujuy", value: "1125" },
                            { text: "Pichincha", value: "1124" },
                            { text: "Pueyrredón", value: "1113" },
                            { text: "Entre Ríos", value: "1123" },
                            { text: "San José", value: "1122" },
                            { text: "Independencia", value: "1121" },
                            { text: "Belgrano", value: "1120" },
                            { text: "Bolivar", value: "1119" },
                            { text: "Correo Central", value: "1166" },
                            { text: "Catalinas", value: "1165" },
                            { text: "Retiro", value: "1164" }
                        ],
                    }
                },
            }
        },
        methods: {
            getForecast() {
                axios.get(this.url)
                    .then((response) => {
                        this.result = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        watch: {
            SelectedLine: function () {
                this.stops = [];
                this.destinations = [];
                this.stops = this.subtes[this.SelectedLine].stops
                this.destinations = this.subtes[this.SelectedLine].cabeceras
                this.SelectedStop = this.stops[0].value;
                this.SelectedDestination = this.destinations[0].value;
            }
        },
        computed: {
            url() {
                return 'transport/forecast/line/' + this.SelectedLine + '/stop/' + this.SelectedStop + '/destination/' + this.SelectedDestination;
            }
        },
        mounted() {
            this.SelectedLine = "LineaA";
            this.stops = this.subtes.LineaA.stops;
            this.destinations = this.subtes.LineaA.cabeceras;
            this.SelectedStop = "1060N";
            this.SelectedDestination = "1";
        }

    }
</script>