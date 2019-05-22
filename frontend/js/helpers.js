const MODULE= Object.freeze({   HOME:000, DASHBOARD:100, HARDWARE:200, SOFTWARE:300, REQUESTS:400, ABOUT:500, });

Vue.mixin({
    methods: {
      addDashToString(str) {
            if (!str) { str = this; }
            return str.toString().trim().replace(/[^a-zA-Z]/g, '-').toLowerCase();
        },
        formatUrl (value) {
            //alert(">>>>>"+value)
            if((value+"").toString().toLowerCase().indexOf("http")<0){
                ///return store.state.url+ "customer/authenticrattanbags/images/"+value;
                return store.state.url+ "images/I/"+value;
            }
            else{return value;}
        },
        formatUrlMedium (value) {
                //alert(">>>>>"+value)
                if((value+"").toString().toLowerCase().indexOf("http")<0){
                    ///return store.state.url+ "customer/authenticrattanbags/images/"+value;
                    return store.state.url+ "images/I/"+(value+"").replace('.jpg','._TR300_.jpg');
                }
                else{return value;}
        },        
        formatUrlSmall (value) {
            //alert(">>>>>"+value)
            if((value+"").toString().toLowerCase().indexOf("http")<0){
                ///return store.state.url+ "customer/authenticrattanbags/images/"+value;
                return store.state.url+ "images/I/"+(value+"").replace('.jpg','._TR106,78_.jpg');
            }
            else{return value;}
        },  
        formatUrlThumb (value) {
            //alert(">>>>>"+value)
            if((value+"").toString().toLowerCase().indexOf("http")<0){
                ///return store.state.url+ "customer/authenticrattanbags/images/"+value;
                return store.state.url+ "images/I/"+(value+"").replace('.jpg','._TR106,78_.jpg');
            }
            else{return value;}
        },  

    }
}) 