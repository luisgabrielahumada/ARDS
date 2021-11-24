<template>
    <div>
        <Widget title="">
            <b-row>
                <b-col>
                    <h2>Informaci&#243;n del canal de venta</h2>
                </b-col>
                <b-col class="text-right">
                    <b-button variant="outline-primary">
                        Guardar
                    </b-button>
                </b-col>
            </b-row>
            <b-overlay :show="isloading" rounded="sm">
                <b-row>
                    <b-col>
                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Codigo del vendedor:" label-for="sellerCode">
                            <b-form-input v-model="model.sellerCode" id="sellerCode" placeholder="Codigo del vendedor" required :state="validateState('sellerCode')"></b-form-input>
                        </b-form-group>
                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Canal de venta:" label-for="salesChannel">
                            <b-form-input v-model="model.salesChannel" id="salesChannel" placeholder="Canal de venta" required :state="validateState('salesChannel')"></b-form-input>
                        </b-form-group>
                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nro. de consecutivo:" label-for="consecutive">
                            <b-form-select id="consecutive" class="mb-3">
                                <b-form-select-option value="callCenter">Call center</b-form-select-option>
                                <b-form-select-option value="distribuidor">Distribuidor</b-form-select-option>
                                <b-form-select-option value="tiendaVirtual">Tienda virtual</b-form-select-option>
                                <b-form-select-option value="retail">Retail</b-form-select-option>
                                <b-form-select-option value="CAV">CAV</b-form-select-option>

                            </b-form-select>
                        </b-form-group>
                        <b-card no-body>
                            <b-tabs fill>
                                <b-tab title="Informacion del cliente">

                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nombre del cliente" label-for="name">
                                        <b-form-input id="name" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Cedula del cliente" label-for="cc">
                                        <b-form-input id="cc" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Ciudad" label-for="city">
                                        <b-form-input id="city" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Departamento" label-for="department">
                                        <b-form-input id="department" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Direccion" label-for="address">
                                        <b-form-input id="address" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Barrio" label-for="neighborhood">
                                        <b-form-input id="neighborhood" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Telefono de contacto" label-for="phone" description="Linea donde se puede contactar">
                                        <b-form-input id="phone" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nro. de min" label-for="min" description="Linea que se esta portando">
                                        <b-form-input id="min" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Alistamiento" label-for="enlistment">
                                        <b-form-input id="enlistment" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Agendamiento cliente" label-for="clientScheduling">
                                        <b-form-input id="clientScheduling" size="Default"></b-form-input>
                                    </b-form-group>
                                </b-tab>

                                <b-tab title="Informacion de entrega">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Franja Entrega:" label-for="Delivery Strip">
                                        <b-form-select id="" class="mb-3">
                                            <b-form-select-option value="AM">AM</b-form-select-option>
                                            <b-form-select-option value="PM">PM</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha Asignacion:" label-for="assignmentDate">
                                        <b-form-datepicker id="assignmentDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha de alistamiento:" label-for="enlistmentDate">
                                        <b-form-datepicker id="enlistmentDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha de salidad a ruta:" label-for="departureDate">
                                        <b-form-datepicker id="departureDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha estimada de entrega" label-for="deliveryDate">
                                        <b-form-datepicker id="deliveryDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Modalidad de entrega:" label-for="deliveryMethod">
                                        <b-form-select id="deliveryMethod" class="mb-3">
                                            <b-form-select-option value="Programada">Programada</b-form-select-option>
                                            <b-form-select-option value="Express">Express</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha de reprogramacion" label-for="rescheduleDate">
                                        <b-form-datepicker id="rescheduleDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Estado de la entrega:" label-for="deliveryStatus">
                                        <b-form-select id="deliveryStatus" class="mb-3">
                                            <b-form-select-option value="Transito">Transito</b-form-select-option>
                                            <b-form-select-option value="Reparto">En Reparto</b-form-select-option>
                                            <b-form-select-option value="Entregado">Entregado</b-form-select-option>
                                            <b-form-select-option value="Devolucion">Devolucion</b-form-select-option>
                                            <b-form-select-option value="Novedad">En Novedad</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>

                                </b-tab>

                                <b-tab title="Trazabilidad">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Origen:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Destino:" label-for="destiny">
                                        <b-form-input id="destiny" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Destinatario:" label-for="addressee">
                                        <b-form-input id="addressee" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Identificacion:" label-for="id">
                                        <b-form-input id="id" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Estado:" label-for="state">
                                        <b-form-input id="state" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Historial:" label-for="record">
                                        <b-form-textarea id="record" placeholder="Historial desde la creacion del servicio" rows="3"></b-form-textarea>
                                    </b-form-group>
                                </b-tab>
                            </b-tabs>
                        </b-card>
                    </b-col>
                </b-row>
            </b-overlay>
        </Widget>
    </div>
</template>

<script>

    import Widget from "@/components/Widget/Widget";
    import { HTTP } from "@/core/Services/http-services";
    import { required } from "vuelidate/lib/validators";

    export default {
        components: {
            Widget,
        },
        data() {
            return {
                isloading: false,
                items: [],
                model: {
                    sellerCode: '',
                    salesChannel: '',
                    consecutive: '',
                },
            }
        },
        validations: {
            model: {
                sellerCode: {
                    required
                },
                salesChannel: {
                    required
                },
                consecutive: {
                    required
                },
            }

        },
        mounted() {
        },
        methods: {
            validateState(name) {
                const { $dirty, $error } = this.$v.model[name];
                return $dirty ? !$error : null;
            },

        },



    };
</script>
