//-- for future use and module expansion
const Home = Vue.component('Home', {
  template:`<div></div>`,
  data() {
    return {
      // textSearch: "nike",
    }
  },
  methods: {
    setPage(pageNumber) {
      this.currentPage = pageNumber
      //alert("pageNumber:"+pageNumber);
      window.history.replaceState(null, null, "?page=" + pageNumber);
    },
  },  //-- methods [end]  
  computed: {
    assets() {
      alert('assets');
      return store.state.datAssets;
    },
    
  },
  created() {
  },
  mounted() {
    //alert('mounted:'+this.currentPage)
    // store.dispatch('fetchProducts')
  },
  updated() {
     
  },

});

 