<template>
    <div class="error-page">
        <b-container>
            <div class="errorContainer mx-auto text-center">
                <h1 class="errorCode">401</h1>
                <p class="errorInfo">
                    Opps, You do not have authorization for this option, please contact your system administrator..
                </p>
                <p class="errorHelp mb-3">
                    If you are sure it should, search for it.
                </p>
            </div>
        </b-container>
    </div>
</template>

<script>
    import { HTTP } from '@/core/Services/http-services';
    export default {
        name: 'not-authorized',
        components: {},
        props: {
           
        },
        data() {
            return {
                
            };
        },
        computed: {
           
        },
        methods: {
           
        },
        mounted() {
        }
    };
</script>
