(function () {
    "use strict";
    var mapperClient = angular.module("mapperClient");

    var stateConfig = function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/login");
        var appRoot = "Scripts/app/";
        $stateProvider.
            state("dashboard", {
                url: "/dashboard",
                templateUrl: appRoot + "dashboard/dashboard.html",
                controller: "dashboardCtrl as vm",
                data: { restricted: true }
            }).
            state("domains", {
                url: "/domains",
                templateUrl: appRoot + "domains/domains.html",
                controller: "domainsCtrl as vm"
            }).
            state("editDomain", {
                url: "/domains/edit/:id",
                templateUrl: appRoot + "domains/edit-domain.html",
                controller: "editDomainCtrl as vm",
                resolve: {
                    domainResource: "domainResource",
                    domain: function (domainResource, $stateParams) {
                        var domainId = $stateParams.id;
                        return domainResource.get({ id: domainId }).$promise;
                    }
                }
            }).
            state("viewDomain", {
                url: "/domains/view/:id",
                templateUrl: appRoot + "domains/view-domain.html",
                controller: "viewDomainCtrl as vm",
                resolve: {
                    domainResource: "domainResource",
                    domain: function (domainResource, $stateParams) {
                        var domainId = $stateParams.id;
                        return domainResource.get({ id: domainId }).$promise;
                    }
                }
            }).

            state("systems", {
                url: "/systems",
                templateUrl: appRoot + "systems/systems.html",
                controller: "systemsCtrl as vm"
                //data: { restricted: true }
            }).
            state("editSystem", {
                url: "/systems/edit/:id",
                templateUrl: appRoot + "systems/edit-system.html",
                controller: "editSystemCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    systemResource: "systemResource",
                    system: function (systemResource, $stateParams) {
                        var systemId = $stateParams.id;
                        return systemResource.get({ id: systemId }).$promise;
                    }
                }
            }).
            state("viewSystem", {
                url: "/systems/view/:id",
                templateUrl: appRoot + "systems/view-system.html",
                controller: "viewSystemCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    systemResource: "systemResource",
                    system: function (systemResource, $stateParams) {
                        var systemId = $stateParams.id;
                        return systemResource.get({ id: systemId }).$promise;
                    }
                }
            }).
            state("addSystem", {
                url: "/systems/add/:id",
                templateUrl: appRoot + "systems/add-system.html",
                controller: "addSystemCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    systemResource: "systemResource",
                    system: function (systemResource, $stateParams) {
                        var systemId = $stateParams.id;
                        return systemResource.get({ id: systemId }).$promise;
                    }
                }
            }).


            state("entities", {
                url: "/entities",
                templateUrl: appRoot + "entities/entities.html",
                controller: "entitiesCtrl as vm"
                //data: { restricted: true }
            }).
            state("editEntity", {
                url: "/entities/edit/:id",
                templateUrl: appRoot + "entities/edit-entity.html",
                controller: "editEntityCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityResource: "entityResource",
                    entity: function (entityResource, $stateParams) {
                        var entityId = $stateParams.id;
                        return entityResource.get({ id: entityId }).$promise;
                    }
                }
            }).
            state("viewEntity", {
                url: "/entities/view/:id",
                templateUrl: appRoot + "entities/view-entity.html",
                controller: "viewEntityCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityResource: "entityResource",
                    entity: function (entityResource, $stateParams) {
                        var entityId = $stateParams.id;
                        return entityResource.get({ id: entityId }).$promise;
                    }
                }
            }).
            state("addEntity", {
                url: "/entities/add/:id",
                templateUrl: appRoot + "entities/add-entity.html",
                controller: "addEntityCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityResource: "entityResource",
                    entity: function (entityResource, $stateParams) {
                        var entityId = $stateParams.id;
                        return entityResource.get({ id: entityId }).$promise;
                    }
                }
            }).

            state("properties", {
                            url: "/properties",
                            templateUrl: appRoot + "properties/properties.html",
                            controller: "propertiesCtrl as vm"
                            //data: { restricted: true }
                        }).
            state("editProperty", {
                url: "/properties/edit/:id",
                templateUrl: appRoot + "properties/edit-property.html",
                controller: "editPropertyCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyResource: "propertyResource",
                    property: function (propertyResource, $stateParams) {
                        var propertyId = $stateParams.id;
                        return propertyResource.get({ id: propertyId }).$promise;
                    }
                }
            }).
            state("viewProperty", {
                url: "/properties/view/:id",
                templateUrl: appRoot + "properties/view-property.html",
                controller: "viewPropertyCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyResource: "propertyResource",
                    property: function (propertyResource, $stateParams) {
                        var propertyId = $stateParams.id;
                        return propertyResource.get({ id: propertyId }).$promise;
                    }
                }
            }).
            state("addProperty", {
                url: "/properties/add/:id",
                templateUrl: appRoot + "properties/add-property.html",
                controller: "addPropertyCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyResource: "propertyResource",
                    property: function (propertyResource, $stateParams) {
                        var propertyId = $stateParams.id;
                        return propertyResource.get({ id: propertyId }).$promise;
                    }
                }
            }).


            state("entityMappings", {
                url: "/entityMappings",
                templateUrl: appRoot + "entity-mappings/entity-mappings.html",
                controller: "entityMappingsCtrl as vm"
                //data: { restricted: true }
            }).
            state("editEntityMapping", {
                url: "/entityMappings/edit/:id",
                templateUrl: appRoot + "entity-mappings/edit-entity-mapping.html",
                controller: "editEntityMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityMappingResource: "entityMappingResource",
                    entityMapping: function (entityMappingResource, $stateParams) {
                        var entityMappingId = $stateParams.id;
                        return entityMappingResource.get({ id: entityMappingId }).$promise;
                    }
                }
            }).
            state("confirmEntityMapping", {
                url: "/entityMappings/confirm/:id",
                templateUrl: appRoot + "entity-mappings/confirm-entity-mapping.html",
                controller: "confirmEntityMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityMappingResource: "entityMappingResource",
                    entityMapping: function (entityMappingResource, $stateParams) {
                        var entityMappingId = $stateParams.id;
                        return entityMappingResource.get({ id: entityMappingId }).$promise;
                    }
                }
            }).
            state("viewEntityMapping", {
                url: "/entityMappings/view/:id",
                templateUrl: appRoot + "entity-mappings/view-entity-mapping.html",
                controller: "viewEntityMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityMappingResource: "entityMappingResource",
                    entityMapping: function (entityMappingResource, $stateParams) {
                        var entityMappingId = $stateParams.id;
                        return entityMappingResource.get({ id: entityMappingId }).$promise;
                    }
                }
            }).
            state("addEntityMapping", {
                url: "/entityMappings/add/:id",
                templateUrl: appRoot + "entity-mappings/add-entity-mapping.html",
                controller: "addEntityMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    entityMappingResource: "entityMappingResource",
                    entityMapping: function (entityMappingResource, $stateParams) {
                        var entityMappingId = $stateParams.id;
                        return entityMappingResource.get({ id: entityMappingId }).$promise;
                    }
                }
            }).
            state("automateEntityMapping", {
                url: "/entityMappings/automate",
                templateUrl: appRoot + "entity-mappings/automate-entity-mapping.html",
                controller: "automatedEntityMappingCtrl as vm"
            }).

            state("propertyMappings", {
                            url: "/propertyMappings",
                            templateUrl: appRoot + "property-mappings/property-mappings.html",
                            controller: "propertyMappingsCtrl as vm"
                            //data: { restricted: true }
                        }).
            state("editPropertyMapping", {
                url: "/propertyMappings/edit/:id",
                templateUrl: appRoot + "property-mappings/edit-property-mapping.html",
                controller: "editPropertyMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyMappingResource: "propertyMappingResource",
                    propertyMapping: function (propertyMappingResource, $stateParams) {
                        var propertyMappingId = $stateParams.id;
                        return propertyMappingResource.get({ id: propertyMappingId }).$promise;
                    }
                }
            }).
            state("viewPropertyMapping", {
                url: "/propertyMappings/view/:id",
                templateUrl: appRoot + "property-mappings/view-property-mapping.html",
                controller: "viewPropertyMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyMappingResource: "propertyMappingResource",
                    propertyMapping: function (propertyMappingResource, $stateParams) {
                        var propertyMappingId = $stateParams.id;
                        return propertyMappingResource.get({ id: propertyMappingId }).$promise;
                    }
                }
            }).
            state("addPropertyMapping", {
                url: "/propertyMappings/add/:id",
                templateUrl: appRoot + "property-mappings/add-property-mapping.html",
                controller: "addPropertyMappingCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    propertyMappingResource: "propertyMappingResource",
                    propertyMapping: function (propertyMappingResource, $stateParams) {
                        var propertyMappingId = $stateParams.id;
                        return propertyMappingResource.get({ id: propertyMappingId }).$promise;
                    }
                }
            }).


            state("mappingOrigins", {
                          url: "/mappingOrigins",
                          templateUrl: appRoot + "mapping-origins/mapping-origins.html",
                          controller: "mappingOriginsCtrl as vm"
                          //data: { restricted: true }
                      }).
            state("editMappingOrigin", {
                url: "/mappingOrigins/edit/:id",
                templateUrl: appRoot + "mapping-origins/edit-mapping-origins.html",
                controller: "editMappingOriginCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    mappingOriginResource: "mappingOriginResource",
                    mappingOrigin: function (mappingOriginResource, $stateParams) {
                        var mappingOriginId = $stateParams.id;
                        return mappingOriginResource.get({ id: mappingOriginId }).$promise;
                    }
                }
            }).
            state("viewMappingOrigin", {
                url: "/mappingOrigins/view/:id",
                templateUrl: appRoot + "mapping-origins/view-mapping-origins.html",
                controller: "viewMappingOriginCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    mappingOriginResource: "mappingOriginResource",
                    mappingOrigin: function (mappingOriginResource, $stateParams) {
                        var mappingOriginId = $stateParams.id;
                        return mappingOriginResource.get({ id: mappingOriginId }).$promise;
                    }
                }
            }).
            state("addMappingOrigin", {
                url: "/mappingOrigins/add/:id",
                templateUrl: appRoot + "mapping-origins/add-mapping-origin.html",
                controller: "addMappingOriginCtrl as vm",
                //data: { restricted: true },
                resolve: {
                    mappingOriginResource: "mappingOriginResource",
                    mappingOrigin: function (mappingOriginResource, $stateParams) {
                        var mappingOriginId = $stateParams.id;
                        return mappingOriginResource.get({ id: mappingOriginId }).$promise;
                    }
                }
            }).


            state("login", {
                url: "/login",
                templateUrl: appRoot + "accounts/login.html",
                controller: "loginCtrl as vm"
            }).
            state("register", {
                url: "/register",
                templateUrl: appRoot + "accounts/register.html",
                controller: "registerCtrl as vm"
            });
    };

    var run = function ($rootScope, currentUser, $state) {
        $rootScope.$on("$stateChangeStart", function (event, toState) {
            if (toState.data && toState.data.restricted && !currentUser.getProfile().isLoggedIn) {
                event.preventDefault();
                $state.go("login");
            }
        });
    };
    mapperClient.config(stateConfig);

    mapperClient.run(run);
})();
