//let url_current="https://restapii.xyz/";
let url_current = "http://localhost:31000/";

const store = new Vuex.Store({
    state: {
        loadingStatus: 'notLoading',
        MODULE: MODULE.HOME,
        datAssets: [],
        // isShowHeader:true,
        url: url_current,
        api: url_current + "api",
    },
    mutations: {
        SET_ASSETS(state, assets) {
            state.datAssets = assets;
        },
        SET_LOADING_STATUS(state, status) {
            state.loadingStatus = status;
        },
        SET_MODULE(state, module) {
            state.MODULE = module;
        },
        SET_SHOW_HEADER(state, status) {
            state.isShowHeader = status;
        },

    },
    actions: {
        async fetchAssets(context) {
            /*
            url = store.state.api + '/assets';
            context.commit('SET_LOADING_STATUS', 'loading')
            await axios.get(url)
                .then(response => {
                    context.commit('SET_LOADING_STATUS', 'Loaded')
                    console.log('response.data.assets', response.data.assets)
                    context.commit('SET_ASSETS', response.data.assets)
                })
                */
               let assets = [{ "assetId": 1, "assetLabel": "Asset 1", "assetDescription": "Description 1", "assetType": "Laptop", "assetStatus": "In-use", "purchaseDate": null, "currentOwner": "John Doe", "deleted": false, "dateCreated": null, "lastUpdated": null },
               { "assetId": 2, "assetLabel": "Asset 2", "assetDescription": "Description 2", "assetType": "Mobile", "assetStatus": "Available", "purchaseDate": null, "currentOwner": "Don", "deleted": false, "dateCreated": null, "lastUpdated": "2019-05-22T01:14:56.497" },
               { "assetId": 3, "assetLabel": "Asset 3", "assetDescription": "Description 3", "assetType": "Laptop", "assetStatus": "In-use", "purchaseDate": null, "currentOwner": "Jane Doe", "deleted": false, "dateCreated": null, "lastUpdated": null },
               { "assetId": 4, "assetLabel": "Asset 4", "assetDescription": "Description 4", "assetType": "Tablet", "assetStatus": "In-repair", "purchaseDate": null, "currentOwner": "Don", "deleted": false, "dateCreated": null, "lastUpdated": "2019-05-22T01:14:51.49" },
               { "assetId": 5, "assetLabel": "Asset 5", "assetDescription": "Description 5", "assetType": "Tablet", "assetStatus": "Available", "purchaseDate": null, "currentOwner": "Don", "deleted": false, "dateCreated": null, "lastUpdated": "2019-05-22T01:06:13.9" },];
                context.commit('SET_ASSETS', assets);
        },
        async assetDelete(context, object) {
            url = store.state.api + '/assets/'+ object.assetId ;
            await axios.delete(url)
              .then(response => {
                console.log('response.data.assets', response.data.assets)
                context.commit('SET_ASSETS', response.data.assets)
              })
        },
        async assetSave(context, object) {
            url = store.state.api + '/assets/';
            await axios.post(url, object.asset)
              .then(response => {
                console.log('response.data.assets', response.data.assets)
                context.commit('SET_ASSETS', response.data.assets);
                app.showModal = false;
              })
        },



        /*
        saveInfo() {
            var arrLatLng= [0, 0];
            latLng=document.getElementById("latLng").innerText;
            if(latLng!=""){
                arrLatLng=latLng.replace(/[()]+/g, "").split(',');
            }
            
            var geolocation=document.getElementById("geolocation").innerText;
            if(this.info.addressType=='home'){
                this.info.homeLocation=geolocation;
                this.info.homeLat =parseFloat(arrLatLng[0]);
                this.info.homeLng= parseFloat(arrLatLng[1]);
            }else{
                this.info.officeLocation=geolocation;
                this.info.officeLat=parseFloat(arrLatLng[0]);
                this.info.officeLng=parseFloat(arrLatLng[1]);
            }

            console.log('info', this.info);

            axios.post(store.state.url + 'save/location',
                    this.info)
            .then((response) => {
                //console.log('response.data: saveLocation', response.data)
                if(location.href.toLowerCase().indexOf("checkout")<0){
                    window.location = "/";
                }else{
                    location.reload();
                }
            })
            .catch((error) => {
                console.error(error)
            });
        },
        
        */ 
        

    },
   

})

