<template>
    <div>
        <Widget title="">
            <b-row>
                <b-col>
                    <h2>Informaci&#243;n del Centro de Atenci&#243;n Tienda o CAV</h2>
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
                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Asesor de Servicio Tecnico:" label-for="technicalAdvisor">
                            <b-form-input v-model="model.technicalAdvisor" id="technicalAdvisor" placeholder="Asesor de Servicio Tecnico" required :state="validateState('technicalAdvisor')"></b-form-input>
                        </b-form-group>

                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Tienda o CAV:" label-for="storeOrCAV">
                            <b-form-input v-model="model.storeOrCAV" id="storeOrCAV" placeholder="Tienda o CAV" required :state="validateState('storeOrCAV')"></b-form-input>
                        </b-form-group>

                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nro. Orden  Servicio:" label-for="serviceOrder">
                            <b-form-input v-model="model.serviceOrder" id="serviceOrder" placeholder="Nro. Orden  Servicio" required :state="validateState('serviceOrder')"></b-form-input>
                        </b-form-group>

                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha ingreso:" label-for="entryDate">
                            <b-form-datepicker id="entryDate" v-model="entryDate" class="mb-2"></b-form-datepicker>
                        </b-form-group>

                        <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Hora ingreso:" label-for="entryHour">
                            <b-form-timepicker v-model="entryHour" locale="en"></b-form-timepicker>
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
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Correo Electronico del Cliente" label-for="min">
                                        <b-form-input id="clientMail" size="Default"></b-form-input>
                                    </b-form-group>
                                </b-tab>
                                <b-tab title="Recoleccion y/o entrega">
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
                                <b-tab title="Trazabilidad Entrega">
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

                                <b-tab title="Datos Equipo">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Marca:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Modelo:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="IMEI:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Servicio:" label-for="service">
                                        <b-form-select id="service" class="mb-3">
                                            <b-form-select-option value="Prepago">Prepago</b-form-select-option>
                                            <b-form-select-option value="Pospago">Pospago</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha Compra o Activacion:" label-for="ativationDate">
                                        <b-form-datepicker id="ativationDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Falla Reportada Por el Cliente:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Espacio Para Comentarios:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Equipo en Garantia?:" label-for="guarantee">
                                        <b-form-checkbox id="guarantee"
                                                         v-model="guarantee"
                                                         name="guarantee"
                                                         value="true"
                                                         unchecked-value="false">
                                            Si
                                        </b-form-checkbox>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Reingreso?:" label-for="reentry">
                                        <b-form-checkbox id="reentry"
                                                         v-model="reentry"
                                                         name="reentry"
                                                         value="true"
                                                         unchecked-value="false">
                                            Si
                                        </b-form-checkbox>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Aplica Equipo de Prestamo?:" label-for="loan">
                                        <b-form-checkbox id="loan"
                                                         v-model="loan"
                                                         name="loan"
                                                         value="true"
                                                         unchecked-value="false">
                                            Si
                                        </b-form-checkbox>
                                    </b-form-group>
                                </b-tab>
                                <b-tab title="Diagnostico y/o Reparacion">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Estado Final Del Equipo:" label-for="finalState">
                                        <b-form-select id="finalState" class="mb-3">
                                            <b-form-select-option value="Reparado">Reparado</b-form-select-option>
                                            <b-form-select-option value="sinReparar">Sin Reparar</b-form-select-option>
                                            <b-form-select-option value="clienteIlocalizable">Cliente Ilocalizable</b-form-select-option>
                                            <b-form-select-option value="clienteNoAutoriza">Cliente No Autoriza</b-form-select-option>
                                            <b-form-select-option value="Cotizado">Cotizado</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Diagnostico Detallado:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Cambio de IMEI?:" label-for="newIMEI">
                                        <b-form-checkbox id="newIMEI"
                                                         v-model="newIMEI"
                                                         name="newIMEI"
                                                         value="true"
                                                         unchecked-value="false">
                                            Si
                                        </b-form-checkbox>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nuevo IMEI:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha Reparacion:" label-for="fixDate">
                                        <b-form-datepicker id="fixDate" class="mb-2"></b-form-datepicker>
                                    </b-form-group>
                                </b-tab>

                                <b-tab title="Check List Control Calidad">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Verificacion de Datos:" label-for="checkData">
                                        <b-form-select id="checkData" class="mb-3">
                                            <b-form-select-option value="sameIMEI">Mismo IMEI</b-form-select-option>
                                            <b-form-select-option value="sinReparar">IMEI Interno Igual al Externo</b-form-select-option>
                                            <b-form-select-option value="SWVersion">Version de SW</b-form-select-option>
                                        </b-form-select>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Pruebas Funcionamiento:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Pruebas Conectividad:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Inspeccion Fisica:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Observaciones:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                </b-tab>

                                <b-tab title="Impresion Orden de Entrega">
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Nombre Tecnico Quien Repara:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Firma Tecnico Quien Repara:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Firma Cliente Recibido a Conformidad:" label-for="origin">
                                        <b-form-input id="origin" size="Default"></b-form-input>
                                    </b-form-group>
                                    <b-form-group label-cols="4" label-cols-lg="3" label-size="Default" label="Fecha De Entrega:" label-for="deliveyDate">
                                        <b-form-datepicker id="deliveyDate" class="mb-2"></b-form-datepicker>
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
                    technicalAdvisor: '',
                    salesChannel: '',
                    serviceOrder: '',
                    entryDate: '',
                    entryHour: '',
                    guarantee: false,
                    reentry: false,
                    loan: false,
                    newIMEI: false,
                },
            }
        },
        validations: {
            model: {
                technicalAdvisor: {
                    required
                },
                storeOrCAV: {
                    required
                },
                serviceOrder: {
                    required
                },
                entryDate: {
                    required
                },
                entryHour: {
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
